using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using CC98.Services.ContentCheck.EaseDun.Native;
using CC98.Services.ContentCheck.EaseDun.Native.Images;
using CC98.Services.ContentCheck.EaseDun.Native.Texts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     提供网易易盾服务。
/// </summary>
public class EaseDunWebService : IContentCheckServiceProvider, IDisposable
{
	/// <summary>
	///     初始化 <see cref="EaseDunWebService" /> 服务的新实例。
	/// </summary>
	/// <param name="options">服务选项。</param>
	/// <param name="contentCheckSettingService">内容审核设置服务。</param>
	/// <param name="logger"></param>
	/// <param name="serviceScopeFactory"></param>
	public EaseDunWebService(IOptions<EaseDunOptions> options,
		AppSettingService<ContentCheckSystemSetting> contentCheckSettingService, ILogger<EaseDunWebService> logger,
		IServiceScopeFactory serviceScopeFactory)
	{
		ContentCheckSettingService = contentCheckSettingService;
		Logger = logger;
		ServiceScopeFactory = serviceScopeFactory;
		Options = options.Value;

		FileExtensionServiceMapping = GenerateFileExtServiceMap();
	}

	/// <summary>
	///     服务相关配置。
	/// </summary>
	private EaseDunOptions Options { get; }

	/// <summary>
	///     内部服务对象。
	/// </summary>
	private EaseDunService InnerService { get; } = new();

	/// <summary>
	///     日志记录服务。
	/// </summary>
	private ILogger<EaseDunWebService> Logger { get; }

	/// <summary>
	///     文件扩展名和服务的对应记录。
	/// </summary>
	private IDictionary<string, ContentCheckServiceType> FileExtensionServiceMapping { get; }

	/// <summary>
	///     随机数生成器。用于生成请求的 <see cref="CommonRequestBody.Nonce" /> 数据。
	/// </summary>
	private Random Random { get; } = new();

	/// <summary>
	///     内容审核服务设置对象。
	/// </summary>
	private AppSettingService<ContentCheckSystemSetting> ContentCheckSettingService { get; }

	/// <summary>
	///     服务容器对象。
	/// </summary>
	private IServiceScopeFactory ServiceScopeFactory { get; }

	/// <summary>
	///     获取的服务名称。该字段为常量。
	/// </summary>
	public static string Name { get; } = "EaseDun";


	/// <inheritdoc />
	public Task<ContentCheckServiceExecutionResult?> ExecutePostCheckAsync(IUserPost post,
		CancellationToken cancellationToken = default)
	{
		return CheckPostAsync(post, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<ContentCheckServiceExecutionResult?> ExecuteFileCheckAsync(IUserFile item,
		CancellationToken cancellationToken = default)
	{
		var serviceType = GetFileServiceType(item.FilePath);

		if (serviceType == null)
		{
			Logger.LogInformation("文件名 {FileName} 无对应的审查服务类型。已跳过该文件的审查。", item.FilePath);
			return null;
		}

		switch (serviceType)
		{
			case ContentCheckServiceType.Image:
				return await CheckImageAsync(item, cancellationToken);
			default:
				Logger.LogWarning("暂时不支持图像以外的文件内容审核服务。文件名：{FileName}, 预期的服务类型：{ServiceType}", item.FilePath, serviceType);
				return null;
		}
	}

	/// <inheritdoc />
	public void Dispose()
	{
		InnerService.Dispose();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	///     从用户提交的文本检测请求中生成请求主体。
	/// </summary>
	/// <param name="info">数据模型。</param>
	/// <param name="extendedInfo">请求相关的任何额外信息。</param>
	/// <returns>需要通过 HTTP 请求传输的表单内容。</returns>
	private TextBatchDetectRequestBody GenerateTextRequestBody(TextCheckInfo info, RequestExtendedInfo? extendedInfo)
	{
		// 将 ImageCheckFileInfo 转换为 ImageItem 对象
		static TextItem CreateItemFromFile(TextCheckItemInfo item)
		{
			return new()
			{
				DataId = item.Id,
				Content = item.Text,
				Title = item.Title,
				PublishTime = item.Time
			};
		}

		return new()
		{
			Version = TextDetectSupportVersions.V5_2,
			SecretId = Options.SecretId,
			BusinessId = Options.Services[ContentCheckServiceType.Text].BusinessId,
			CheckLabels = string.Join(",", info.CheckTypes),
			Nonce = Random.NextInt64(),
			Timestamp = DateTimeOffset.Now,
			Texts = info.Items.Select(CreateItemFromFile).ToArray(),

			ExtendedInfo = extendedInfo ?? new()
		};
	}

	/// <summary>
	///     从 <see cref="ImageCheckInfo" /> 产生 <see cref="ImageDetectRequestBody" /> 对象。
	/// </summary>
	/// <param name="info">包含所有图像检测算法必须信息的 <see cref="ImageCheckInfo" /> 对象。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含用于发送图像检测请求的 <see cref="ImageDetectRequestBody" /> 对象。</returns>
	private async Task<ImageDetectRequestBody> GenerateImageRequestBodyCoreAsync(ImageCheckInfo info,
		RequestExtendedInfo? extendedInfo, CancellationToken cancellationToken = default)
	{
		// 将数据流转换为 BASE64 字符串
		static async Task<string> ConvertContentToBase64Async(Stream input,
			CancellationToken cancellationToken = default)
		{
			await using var crypto = new CryptoStream(input, new ToBase64Transform(), CryptoStreamMode.Read);
			using var streamReader = new StreamReader(crypto, Encoding.UTF8, leaveOpen: true);
			return await streamReader.ReadToEndAsync(cancellationToken);
		}

		// 将 ImageCheckFileInfo 转换为 ImageItem 对象
		static async Task<ImageItem> CreateItemFromFileAsync(ImageCheckFileInfo file,
			CancellationToken cancellationToken = default)
		{
			return new()
			{
				DataId = file.Id,
				Name = file.Name,
				Type = ImageDataType.Base64,
				Data = await ConvertContentToBase64Async(file.Content, cancellationToken)
			};
		}

		// 创建图像对象
		var images = new List<ImageItem>();
		foreach (var file in info.Files) images.Add(await CreateItemFromFileAsync(file, cancellationToken));

		return new()
		{
			Version = ImageDetectSupportedVersions.V5_1,
			SecretId = Options.SecretId,
			BusinessId = Options.Services[ContentCheckServiceType.Image].BusinessId,
			CheckLabels = string.Join(",", info.CheckTypes),
			Nonce = Random.NextInt64(),
			Timestamp = DateTimeOffset.Now,
			Images = images.ToArray(),

			ExtendedInfo = extendedInfo ?? new(),

			PublishTime = info.PublishTime
		};
	}

	/// <summary>
	///     执行文本检测功能的核心方法。
	/// </summary>
	/// <param name="info">数据模型。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为文本检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public Task<TextDetectItemResult[]> CheckTextAsync(TextCheckInfo info, RequestExtendedInfo? extendedInfo,
		CancellationToken cancellationToken = default)
	{
		var requestBody = GenerateTextRequestBody(info, extendedInfo);
		return InnerService.BatchCheckTextAsync(requestBody, Options.SecretKey, cancellationToken);
	}

	/// <summary>
	///     执行图像检测的核心方法。
	/// </summary>
	/// <param name="model">数据模型对象。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为图像检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<ImageDetectResult[]> CheckImageAsync(ImageCheckInfo model, RequestExtendedInfo? extendedInfo,
		CancellationToken cancellationToken = default)
	{
		var requestBody = await GenerateImageRequestBodyCoreAsync(model, extendedInfo, cancellationToken);
		return await InnerService.CheckImageAsync(requestBody, Options.SecretKey, cancellationToken);
	}

	/// <summary>
	///     获取特定审核类型的默认审核标签。
	/// </summary>
	/// <param name="serviceType">审核类型。</param>
	/// <returns><paramref name="serviceType" /> 对应的所有审核标签的集合。如果返回 <c>null</c>，则表示对该类型禁用了审核。</returns>
	/// <exception cref="InvalidOperationException">系统后台未正确配置审核标签。</exception>
	public int[]? GetDefaultLabels(ContentCheckServiceType serviceType)
	{
		if (ContentCheckSettingService.Current.Global.CheckTypes.TryGetValue(serviceType, out var setting))
			switch (setting.CheckMode)
			{
				case ContentCheckTypeMode.All:
					return Options.Services[serviceType].EnabledLabels;
				case ContentCheckTypeMode.Disabled:
					return null;
				case ContentCheckTypeMode.Custom:
					return setting.Labels
						   ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture,
							   "系统后台针对服务类型 {0} 配置了自定义审核模式，但实际未设置任何审审核标签。", serviceType));
			}

		throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, "系统后台没有针对服务类型 {0} 正确配置审核设置。",
			serviceType));
	}


	/// <summary>
	///     从 <see cref="AppSetting" /> 构建扩展名到服务类型的映射关系表。
	/// </summary>
	/// <returns>一个映射关系的字典。其中键为文件扩展名，值为对应的服务类型。</returns>
	private IDictionary<string, ContentCheckServiceType> GenerateFileExtServiceMap()
	{
		return
			(from item in Options.Services
			 let serviceType = item.Key
			 let setting = item.Value
			 from ext in setting.FileExtensions
			 select new
			 {
				 Type = serviceType,
				 Ext = ext
			 })
			.ToDictionary(i => i.Ext, i => i.Type, StringComparer.OrdinalIgnoreCase);
	}

	/// <summary>
	///     尝试从文件名中推测需要使用的内容审查服务类型。如果无法推测服务类型，则返回 <c>null</c>。
	/// </summary>
	/// <param name="fileName">文件名称。</param>
	/// <returns><paramref name="fileName" /> 对应的服务类型。如果无法推测服务类型，则返回 <c>null</c>。</returns>
	private ContentCheckServiceType? GetFileServiceType(string fileName)
	{
		return FileExtensionServiceMapping.TryGetValue(Path.GetExtension(fileName), out var type)
			? type
			: null;
	}

	/// <summary>
	///     对用户的发言内容执行审查。
	/// </summary>
	/// <param name="item">要检查的发言记录。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。</returns>
	private async Task<ContentCheckServiceExecutionResult?> CheckPostAsync(IUserPost item,
		CancellationToken cancellationToken = default)
	{
		// 获取文本类型的默认审核类型
		var defaultCheckTypes = GetDefaultLabels(ContentCheckServiceType.Text);

		// 默认禁用文本审核，则不进行任何操作。
		if (defaultCheckTypes == null) return null;

		var checkInfo = new TextCheckInfo
		{
			CheckTypes = defaultCheckTypes,
			Items = new TextCheckItemInfo[]
			{
				new()
				{
					Id = item.Id.ToString(),
					Text = item.Content,
					Title = item.Title,
					Time = item.Time
				}
			}
		};

		// 扩展业务信息
		var extendedInfo =
			await GenerateExtendInfoFromUserIdAsync(item.UserId, cancellationToken)
			?? new();

		// 包含在发帖内容中的额外扩展数据。
		extendedInfo.Ip = item.Ip;
		extendedInfo.Topic = item.TopicId.ToString();
		extendedInfo.CommentId = item.ParentId?.ToString();

		try
		{
			var result = (await CheckTextAsync(checkInfo, extendedInfo, cancellationToken))
				.SingleOrDefault()
				?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture,
					"对回复 {0} 进行审查时，服务器无法正确返回结果。", item.Id));

			return ToResult(result.AntiSpam, ContentCheckServiceType.Text);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(
				string.Format(CultureInfo.CurrentUICulture, "执行检查时发生错误。错误消息：{0}", ex.Message), ex);
		}
	}


	/// <summary>
	///     执行图片检查的核心方法。
	/// </summary>
	/// <param name="item">要检查的图片对应的上传记录。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为本次审查的相关执行信息。如果未进行任何审查，则该信息为 <c>null</c>。</returns>
	private async Task<ContentCheckServiceExecutionResult?> CheckImageAsync(IUserFile item,
		CancellationToken cancellationToken = default)
	{
		// 获取图像类型的默认审查分类。
		var defaultCheckTypes = GetDefaultLabels(ContentCheckServiceType.Image);

		// 如果默认审查类型为 null 则表示禁用审查。将不执行任何操作。
		if (defaultCheckTypes == null) return null;

		// 打开文件以获取数据流
		await using var fileStream = File.OpenRead(item.FilePath);

		// 基本审查信息
		var checkInfo = new ImageCheckInfo
		{
			CheckTypes = defaultCheckTypes,
			PublishTime = item.UploadTime,

			// 单个图片
			Files = new ImageCheckFileInfo[]
			{
				new()
				{
					Id = item.Id.ToString(),
					Name = item.FilePath,
					Content = fileStream
				}
			}
		};

		// 扩展审查信息
		var extendedInfo =
			await GenerateExtendInfoFromUserIdAsync(item.UploadUserId, cancellationToken);

		try
		{
			// 执行审查
			var result =
				(await CheckImageAsync(checkInfo, extendedInfo, cancellationToken)).SingleOrDefault()
			?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture,
					"为图片 {0} 进行审查时，服务器无法正确返回结果。", item.FilePath));

			return ToResult(result.AntiSpam, ContentCheckServiceType.Image);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(
				string.Format(CultureInfo.CurrentUICulture, "执行检查时发生错误。错误消息：{0}", ex.Message), ex);
		}
	}

	/// <summary>
	///     根据用户数据创建审核操作所需的扩展数据。
	/// </summary>
	/// <param name="userId">用户标识。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为请求相关的扩展数据。</returns>
	private async Task<RequestExtendedInfo?> GenerateExtendInfoFromUserIdAsync(int userId,
		CancellationToken cancellationToken = default)
	{
		await using var scope = ServiceScopeFactory.CreateAsyncScope();
		var userInfoCache = scope.ServiceProvider.GetService<IUserInfoService>();

		if (userInfoCache == null)
		{
			Logger.LogWarning("未获取用户信息服务，将不会生成扩展信息。");
			return null;
		}

		var userInfo = await userInfoCache.GetUserInfoAsync(userId, cancellationToken);

		if (userInfo == null) return null;

		return new()
		{
			Account = userInfo.Id.ToString(),
			NickName = userInfo.Name,
			Age =
				userInfo.BirthDay == null
					? null
					: DateTime.Today.Year - userInfo.BirthDay.Value.Year,
			Gender = userInfo.Gender switch
			{
				Gender.Female => Native.Gender.Female,
				Gender.Male => Native.Gender.Male,
				_ => Native.Gender.Unknown
			},
			RegisterTime = userInfo.RegisterTime,
			FanCount = userInfo.FanCount,
			Phone = userInfo.PhoneNumber
		};
	}


	/// <summary>
	///     将网易易盾审查结果转换为通用的结果信息。
	/// </summary>
	/// <param name="info">要转换的 <see cref="IAntiSpamInfo" /> 对象。</param>
	/// <param name="serviceType">使用的服务类型。</param>
	/// <returns>转换后的 <see cref="ContentCheckServiceExecutionResult" /> 对象。</returns>
	private static ContentCheckServiceExecutionResult ToResult(IAntiSpamInfo info, ContentCheckServiceType serviceType)
	{
		return new()
		{
			Result = info.Suggestion switch
			{
				ItemResultSuggestion.Pass => ContentCheckResult.Pass,
				ItemResultSuggestion.Suspect => ContentCheckResult.Undetermined,
				ItemResultSuggestion.Fail => ContentCheckResult.Failed,
				_ => ContentCheckResult.Error
			},

			ServiceType = serviceType,

			Request = null,
			Response = JsonSerializer.Serialize(info, new JsonSerializerOptions(JsonSerializerDefaults.General))
		};
	}
}