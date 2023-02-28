using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using CC98.Services.ContentCheck.EaseDun.Native;
using CC98.Services.ContentCheck.EaseDun.Native.Images;
using CC98.Services.ContentCheck.EaseDun.Native.Texts;

using JetBrains.Annotations;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     提供网易易盾服务。
/// </summary>
[PublicAPI]
public class EaseDunService : IDisposable
{
	/// <summary>
	///     初始化 <see cref="EaseDunService" /> 服务的新实例。
	/// </summary>
	public EaseDunService()
	{
		HttpClient = new()
		{
			BaseAddress = new(EaseDunConstants.ServiceBaseUri)
		};
	}

	/// <summary>
	///     HTTP 请求对象。
	/// </summary>
	private HttpClient HttpClient { get; }

	/// <inheritdoc />
	public void Dispose()
	{
		HttpClient.Dispose();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	///     执行单项文本检测。
	/// </summary>
	/// <param name="request">文本检测的请求内容。</param>
	/// <param name="secretKey">用于加密数据的加密密钥。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为文本检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<TextDetectItemResult> CheckTextAsync(TextSingleDetectRequestBody request, string secretKey,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var responseMessage = await HttpClient.PostAsync(EaseDunConstants.TextCheckUri,
				new FormUrlEncodedContent(request.GenerateSignature(secretKey)), cancellationToken);

			var data = await responseMessage.Content.ReadFromJsonAsync<CommonResponseBody<TextDetectItemResult>>(
						   new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken)
					   ?? throw new InvalidOperationException("无法从服务器端获得响应。");

			if (data.Code != 200) throw new InvalidOperationException(data.Message);

			return data.Result!;
		}
		catch (Exception ex)
		{
			throw new EaseDunServiceException("无法获得网易易盾服务器响应结果。", ex);
		}

	}

	/// <summary>
	///     执行批量文本检测。
	/// </summary>
	/// <param name="request">文本检测的请求内容。</param>
	/// <param name="secretKey">用于加密数据的加密密钥。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为文本检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<TextDetectItemResult[]> BatchCheckTextAsync(TextBatchDetectRequestBody request, string secretKey,
	CancellationToken cancellationToken = default)
	{
		try
		{
			var responseMessage = await HttpClient.PostAsync(EaseDunConstants.TextBatchCheckUri,
				new FormUrlEncodedContent(request.GenerateSignature(secretKey)), cancellationToken);


			var data = await responseMessage.Content.ReadFromJsonAsync<CommonResponseBody<TextDetectItemResult[]>>(
						   new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken)
					   ?? throw new InvalidOperationException("无法从服务器端获得响应。");

			if (data.Code != 200) throw new InvalidOperationException(data.Message);

			return data.Result!;
		}
		catch (Exception ex)
		{
			throw new EaseDunServiceException("无法获得网易易盾服务器响应结果。", ex);
		}
	}

	/// <summary>
	///     执行图像检测。
	/// </summary>
	/// <param name="request">图像检测的请求内容。</param>
	/// <param name="secretKey">用于加密数据的加密密钥。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果为图像检测结果。</returns>
	/// <exception cref="InvalidOperationException">操作过程中发生错误。</exception>
	public async Task<ImageDetectResult[]> CheckImageAsync(ImageDetectRequestBody request, string secretKey,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var responseMessage = await HttpClient.PostAsync(EaseDunConstants.ImageCheckUri,
				new FormUrlEncodedContent(request.GenerateSignature(secretKey)), cancellationToken);

			var data = await responseMessage.Content.ReadFromJsonAsync<CommonResponseBody<ImageDetectResult[]>>(
						   new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken)
					   ?? throw new InvalidOperationException("无法从服务器端获得响应。");

			if (data.Code != 200) throw new InvalidOperationException(data.Message);

			return data.Result!;
		}
		catch (Exception ex)
		{
			throw new EaseDunServiceException("无法获得网易易盾服务器响应结果。", ex);
		}
	}
}