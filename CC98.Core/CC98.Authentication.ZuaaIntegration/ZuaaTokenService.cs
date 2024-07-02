using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using JetBrains.Annotations;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 提供浙大校友令牌身份验证服务。
/// </summary>
public class ZuaaTokenService(IOptions<ZuaaTokenServiceOptions> options, IHttpClientFactory httpClientFactory)
{
	/// <summary>
	/// 创建其他业务需要使用的 <see cref="HttpClient"/> 对象。
	/// </summary>
	/// <returns>创建的 <see cref="HttpClient"/> 对象。</returns>
	[MustDisposeResource]
	private HttpClient CreateHttpClient()
	{
		var client = CreateHttpClientByName(options.Value.HttpClientName);
		client.BaseAddress = new(options.Value.BaseUri);

		return client;
	}

	/// <summary>
	/// 根据配置名称创建对应的 <see cref="HttpClient"/> 对象。
	/// </summary>
	/// <param name="name">用于创建 <see cref="HttpClient"/> 的配置名称。如果该参数为 <c>null</c> 则表示使用默认配置。</param>
	/// <returns>创建的 <see cref="HttpClient"/> 对象。</returns>
	[MustDisposeResource]
	private HttpClient CreateHttpClientByName(string? name)
	{
		return name != null ? httpClientFactory.CreateClient(name) : httpClientFactory.CreateClient();
	}

	/// <summary>
	/// 发送 HTTP 请求并获得响应数据。
	/// </summary>
	/// <typeparam name="TData">响应的实际数据类型。</typeparam>
	/// <param name="method">要使用的 HTTP 方法。</param>
	/// <param name="baseUrl">请求的基础 URL。</param>
	/// <param name="query">要附加的请求参数。</param>
	/// <param name="body">要提供的请求主体。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>响应中包含的数据。</returns>
	private Task<TData> GetResponseAsync<TData>(HttpMethod method,
		[StringSyntax(StringSyntaxAttribute.Uri)] string baseUrl, object? query = null, object? body = null,
		CancellationToken cancellationToken = default)
	{
		// 将查询参数对象转换为参数集合
		var queryArgs =
			(from i in query.ObjectToDictionary()
			 let valueString = i.Value?.ToString()
			 select (i.Key, valueString)).ToDictionary();

		// 构造查询参数
		var url = QueryHelpers.AddQueryString(baseUrl, queryArgs);

		// 构造消息
		var message = new HttpRequestMessage(method, baseUrl);

		// 如果提供了请求主体，则设置请求主体数据
		if (body != null)
		{
			message.Content = JsonContent.Create(body);
		}

		// 获得响应
		return GetResponseDataCoreAsync<TData>(message, cancellationToken);
	}

	/// <summary>
	/// 向浙大校友发送服务器响应的核心方法。
	/// </summary>
	/// <typeparam name="TData">响应的数据类型。</typeparam>
	/// <param name="message">要发送的 HTTP 请求消息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>响应中包含的核心数据。</returns>
	/// <exception cref="ZuaaTokenServiceException">发送或接受响应过程中发生错误。</exception>
	private async Task<TData> GetResponseDataCoreAsync<TData>(HttpRequestMessage message, CancellationToken cancellationToken = default)
	{
		using var client = CreateHttpClient();

		try
		{
			// 发送 HTTP 请求并确保服务器响应
			var response = await client.SendAsync(message, cancellationToken);
			response.EnsureSuccessStatusCode();

			// 提取内容并确保内容为正确响应
			var data = await response.Content.ReadFromJsonAsync<ZuaaTokenServiceResponse<TData>>(cancellationToken)
					   ?? throw new ZuaaTokenServiceException("无法解析浙大校友服务器提供的响应内容。"); 
			data.EnsureSuccess();

			return data.Data;
		}
		catch (HttpRequestException ex)
		{
			throw new ZuaaTokenServiceException("和浙大校友服务器通信时发生错误，无法获取响应。", ex);
		}
	}

	/// <summary>
	/// 检查给定的令牌是否有效。
	/// </summary>
	/// <param name="token">要检查的令牌值。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含一个值，指示 <paramref name="token"/> 是否为有效值。</returns>
	public async Task<bool> CheckTokenAsync(string token, CancellationToken cancellationToken = default)
	{
		var responseData = await GetResponseAsync<int>(HttpMethod.Get, "check", new { token }, null, cancellationToken);
		return responseData == 1;
	}

	/// <summary>
	/// 获取给定令牌的详细信息。
	/// </summary>
	/// <param name="token">要检查的令牌值。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含 <paramref name="token"/> 对应的用户详细信息。如果 <paramref name="token"/> 为无效值，则结果为 <c>null</c>。</returns>
	public Task<ZuaaTokenDetailInfo?> GetTokenDetailAsync(string token, CancellationToken cancellationToken = default)
	{
		return GetResponseAsync<ZuaaTokenDetailInfo?>(HttpMethod.Get, "detail", new { token }, null, cancellationToken);
	}

	/// <summary>
	/// 比较给定的信息是否和校友数据库中的信息一致。
	/// </summary>
	/// <param name="token">登录用户的令牌值。</param>
	/// <param name="name">要比较的用户的姓名。</param>
	/// <param name="phone">要比较的用户的电话号码。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含一个值，指示 <paramref name="token"/> 对应的用户，其校友数据库中的姓名和电话是否与 <paramref name="name"/> 和 <paramref name="phone"/> 提供的值完全一致。</returns>
	public Task<bool> CompareAsync(string token, string name, string phone, CancellationToken cancellationToken)
	{
		return GetResponseAsync<bool>(HttpMethod.Post, "compare", null, new { token, name, phone }, cancellationToken);
	}
}