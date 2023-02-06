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
using Microsoft.Extensions.Options;

namespace CC98.Management.Services.EaseDun;

/// <summary>
///     提供网易易盾服务的 Web 实现。
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
		HttpClient = new()
		{
			BaseAddress = new(options.Value.BaseUri)
		};
	}

	/// <summary>
	///     服务相关配置。
	/// </summary>
	public EaseDunOptions Options { get; }

	/// <summary>
	///     HTTP 请求对象。
	/// </summary>
	private HttpClient HttpClient { get; }

	/// <summary>
	///     随机数生成器。用于生成请求的 <see cref="CommonRequestBody.Nonce"/> 数据。
	/// </summary>
	private Random Random { get; } = new();

	/// <inheritdoc />
	public void Dispose()
	{
		HttpClient.Dispose();
	}

	/// <summary>
	/// 从用户提交的文本检测请求中生成请求主体。
	/// </summary>
	/// <param name="model">数据模型。</param>
	/// <returns>需要通过 HTTP 请求传输的表单内容。</returns>
	private IDictionary<string, string> GenerateRequestBody(TextDetectModel model)
	{
		var result = new TextBatchDetectRequestBody
		{
			Version = TextDetectSupportVersions.V5_2,
			SecretId = Options.SecretId,
			BusinessId = Options.Services[ContentCheckServiceType.Text].BusinessId,
			CheckLabels = string.Join(",", model.CheckTypes),
			Nonce = Random.NextInt64(),
			Timestamp = DateTimeOffset.Now,
			Texts = new[]
			{
				new TextItem
				{
					DataId = Random.NextInt64().ToString("D"),
					Content = model.Text,
					PublishTime = DateTimeOffset.Now
				}
			}
		};

		return result.GenerateSignature(Options.SecretKey);
	}

	private async Task<IDictionary<string, string>> GenerateRequestBodyAsync(ImageDetectModel model, CancellationToken cancellationToken = default)
	{
		static async Task<string> ConvertContentToBase64Async(IFormFile file, CancellationToken cancellationToken = default)
		{
			await using var input = file.OpenReadStream();
			await using var crypto = new CryptoStream(input, new ToBase64Transform(), CryptoStreamMode.Read);
			using var streamReader = new StreamReader(crypto, Encoding.UTF8, leaveOpen: true);
			return await streamReader.ReadToEndAsync(cancellationToken);
		}

		static async Task<ImageItem> CreateItemFromFileAsync(IFormFile file, CancellationToken cancellationToken = default)
		{
			return new()
			{
				Name = file.FileName,
				Type = ImageDataType.Base64,
				Data = await ConvertContentToBase64Async(file, cancellationToken)
			};
		}

		// 创建图像对象
		var images = new List<ImageItem>();
		foreach (var file in model.Files)
		{
			images.Add(await CreateItemFromFileAsync(file, cancellationToken));
		}

		var result = new ImageDetectRequestBody
		{
			Version = ImageDetectSupportedVersions.V5_1,
			SecretId = Options.SecretId,
			BusinessId = Options.Services[ContentCheckServiceType.Image].BusinessId,
			CheckLabels = string.Join(",", model.CheckTypes),
			Nonce = Random.NextInt64(),
			Timestamp = DateTimeOffset.Now,
			Images = images.ToArray()
		};

		return result.GenerateSignature(Options.SecretKey);
	}

	/// <summary>
	/// 执行文本检测功能的核心方法。
	/// </summary>
	/// <param name="model">数据模型。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为文本检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<TextAntiSpamInfo> CheckTextAsync(TextDetectModel model, CancellationToken cancellationToken = default)
	{
		var requestData = GenerateRequestBody(model);

		var responseMessage = await HttpClient.PostAsync("http://as.dun.163.com/v5/text/batch-check",
			new FormUrlEncodedContent(requestData), cancellationToken);


		var data = await responseMessage.Content.ReadFromJsonAsync<CommonResponseBody<TextDetectItemResult[]>>(
			new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);

		if (data == null) throw new InvalidOperationException("无法从服务器端获得响应。");

		if (data.Code != 200) throw new InvalidOperationException(data.Message);

		return data.Result!.Single().AntiSpam;
	}

	/// <summary>
	/// 执行图像检测的核心方法。
	/// </summary>
	/// <param name="model">数据模型对象。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为图像检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<IEnumerable<ImageAntiSpamInfo>> CheckImageAsync(ImageDetectModel model, CancellationToken cancellationToken = default)
	{
		var requestData = await GenerateRequestBodyAsync(model, cancellationToken);

		var responseMessage = await HttpClient.PostAsync("http://as.dun.163.com/v5/image/check",
			new FormUrlEncodedContent(requestData), cancellationToken);


		var data = await responseMessage.Content.ReadFromJsonAsync<CommonResponseBody<ImageDetectResult[]>>(
			new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);

		if (data == null) throw new InvalidOperationException("无法从服务器端获得响应。");

		if (data.Code != 200) throw new InvalidOperationException(data.Message);

		return data.Result!.Select(i => i.AntiSpam);
	}
}