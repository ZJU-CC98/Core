using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using CC98.Services.ContentCheck;
using CC98.Services.ContentCheck.EaseDun.Native;
using CC98.Services.ContentCheck.EaseDun.Native.Images;
using CC98.Services.ContentCheck.EaseDun.Native.Texts;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     提供网易易盾服务。
/// </summary>
public class EaseDunWebService : IDisposable
{
	/// <summary>
	///     初始化 <see cref="EaseDunWebService" /> 服务的新实例。
	/// </summary>
	/// <param name="options">服务选项。</param>
	public EaseDunWebService(IOptions<EaseDunOptions> options)
	{
		Options = options.Value;
	}

	/// <summary>
	///     服务相关配置。
	/// </summary>
	private EaseDunOptions Options { get; }

	/// <summary>
	/// 内部服务对象。
	/// </summary>
	private EaseDunService InnerService { get; } = new();

	/// <summary>
	///     随机数生成器。用于生成请求的 <see cref="CommonRequestBody.Nonce"/> 数据。
	/// </summary>
	private Random Random { get; } = new();

	/// <inheritdoc />
	public void Dispose()
	{
		InnerService.Dispose();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// 从用户提交的文本检测请求中生成请求主体。
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
				PublishTime = item.Time,
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
	/// 从 <see cref="ImageCheckInfo"/> 产生 <see cref="ImageDetectRequestBody"/> 对象。
	/// </summary>
	/// <param name="info">包含所有图像检测算法必须信息的 <see cref="ImageCheckInfo"/> 对象。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含用于发送图像检测请求的 <see cref="ImageDetectRequestBody"/> 对象。</returns>
	private async Task<ImageDetectRequestBody> GenerateImageRequestBodyCoreAsync(ImageCheckInfo info, RequestExtendedInfo? extendedInfo, CancellationToken cancellationToken = default)
	{
		// 将数据流转换为 BASE64 字符串
		static async Task<string> ConvertContentToBase64Async(Stream input, CancellationToken cancellationToken = default)
		{
			await using var crypto = new CryptoStream(input, new ToBase64Transform(), CryptoStreamMode.Read);
			using var streamReader = new StreamReader(crypto, Encoding.UTF8, leaveOpen: true);
			return await streamReader.ReadToEndAsync(cancellationToken);
		}

		// 将 ImageCheckFileInfo 转换为 ImageItem 对象
		static async Task<ImageItem> CreateItemFromFileAsync(ImageCheckFileInfo file, CancellationToken cancellationToken = default)
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
		foreach (var file in info.Files)
		{
			images.Add(await CreateItemFromFileAsync(file, cancellationToken));
		}

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
	/// 执行文本检测功能的核心方法。
	/// </summary>
	/// <param name="info">数据模型。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为文本检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public Task<TextDetectItemResult[]> CheckTextAsync(TextCheckInfo info, RequestExtendedInfo? extendedInfo, CancellationToken cancellationToken = default)
	{
		var requestBody = GenerateTextRequestBody(info, extendedInfo);
		return InnerService.BatchCheckTextAsync(requestBody, Options.SecretKey, cancellationToken);
	}

	/// <summary>
	/// 执行图像检测的核心方法。
	/// </summary>
	/// <param name="model">数据模型对象。</param>
	/// <param name="extendedInfo">请求的扩展信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为图像检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<ImageDetectResult[]> CheckImageAsync(ImageCheckInfo model, RequestExtendedInfo? extendedInfo, CancellationToken cancellationToken = default)
	{
		var requestBody = await GenerateImageRequestBodyCoreAsync(model, extendedInfo, cancellationToken);
		return await InnerService.CheckImageAsync(requestBody, Options.SecretKey, cancellationToken);
	}
}