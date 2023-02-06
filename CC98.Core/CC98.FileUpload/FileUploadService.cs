using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CC98.Identity;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace CC98.Services;

/// <summary>
/// 提供 CC98 文件上传服务实现。
/// </summary>
public class FileUploadService
{

	/// <summary>
	/// 上传的配置设置。
	/// </summary>
	public FileUploadServiceConfig Config { get; }

	/// <summary>
	/// 客户端令牌响应数据。
	/// </summary>
	private TokenResponse TokenResponse { get; set; }

	/// <summary>
	/// 发现文档响应数据。
	/// </summary>
	private DiscoveryDocumentResponse DiscoveryDocumentResponse { get; set; }

	/// <summary>
	/// 初始化 <see cref="FileUploadService"/> 服务的新实例。
	/// </summary>
	/// <param name="config">服务配置信息。</param>
	public FileUploadService(FileUploadServiceConfig config)
	{
		Config = config ?? throw new ArgumentNullException(nameof(config));
	}

	/// <summary>
	/// 执行初始化操作。
	/// </summary>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。</returns>
	public Task InitializeAsync(CancellationToken cancellationToken = default)
	{
		return GetTokenCoreAsync(cancellationToken);
	}

	/// <summary>
	/// 向服务器检索令牌。
	/// </summary>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。</returns>
	private async Task GetTokenCoreAsync(CancellationToken cancellationToken = default)
	{
		await EnsureDiscoveryDocumentAsync(cancellationToken);

		using (var httpClient = new HttpClient())
		{
			var request = new ClientCredentialsTokenRequest
			{
				Address = DiscoveryDocumentResponse.TokenEndpoint,
				ClientId = Config.ClientId,
				ClientSecret = Config.ClientSecret,
				Scope = GetFinalScope(),
			};

			var response = await httpClient.RequestClientCredentialsTokenAsync(request, cancellationToken);

			if (response.IsError)
			{
				throw new InvalidOperationException(response.ErrorDescription ?? response.Error, response.Exception);
			}

			TokenResponse = response;
		}
	}

	/// <summary>
	/// 获取服务器响应文档。
	/// </summary>
	/// <returns>表示异步操作的任务。</returns>
	private async Task GetDiscoveryDocumentCoreAsync(CancellationToken cancellationToken = default)
	{
		using (var httpClient = new HttpClient())
		{
			var response = await httpClient.GetDiscoveryDocumentAsync(Config.Authority, cancellationToken);

			if (response.IsError)
			{
				throw new InvalidOperationException(response.Error, response.Exception);
			}

			DiscoveryDocumentResponse = response;
		}
	}

	/// <summary>
	/// 确保 <see cref="DiscoveryDocumentResponse"/> 已经存在，如果不存在则从服务器已下载。
	/// </summary>
	/// <returns>表示异步操作的任务。</returns>
	private async Task EnsureDiscoveryDocumentAsync(CancellationToken cancellationToken = default)
	{
		if (DiscoveryDocumentResponse == null || DiscoveryDocumentResponse.IsError)
		{
			await GetDiscoveryDocumentCoreAsync(cancellationToken);
		}
	}


	/// <summary>
	/// 获取 scope 参数的最终值。
	/// </summary>
	/// <returns>scope 参数的最终值。</returns>
	private string GetFinalScope()
	{
		var scopeList = new HashSet<string>(Config.AdditionalScopes ?? Array.Empty<string>())
		{
			ApiScopes.FileUpload
		};


		if (Config.RequestOfflineAccess)
		{
			scopeList.Add(OidcConstants.StandardScopes.OfflineAccess);
		}

		return string.Join(" ", scopeList.ToArray());
	}

	/// <summary>
	/// 确保令牌有效。否则则引发异常。
	/// </summary>
	private async Task EnsureTokenResponseAsync(CancellationToken cancellationToken = default)
	{
		if (TokenResponse == null || TokenResponse.IsError)
		{
			await GetTokenCoreAsync(cancellationToken);
		}
	}

	/// <summary>
	/// 向服务器上传一个或多个文件。
	/// </summary>
	/// <param name="files">要上传的文件的集合。</param>
	/// <param name="subPath">上传子路径。</param>
	/// <param name="compressImages">是否压缩图像。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含上传后实际可访问的文件的路径。</returns>
	public async Task<IEnumerable<string>> UploadAsync(IEnumerable<UploadFileInfo> files, string subPath, bool compressImages, CancellationToken cancellationToken = default)
	{
		// 检查令牌。
		await EnsureTokenResponseAsync(cancellationToken);

		using (var httpClient = new HttpClient())
		{
			httpClient.DefaultRequestHeaders.Authorization =
				new(TokenResponse.TokenType, TokenResponse.AccessToken);

			var fileContent = new MultipartFormDataContent();

				

			foreach (var file in files)
			{
				fileContent.Add(new StreamContent(file.Stream), Config.FileFormKey, file.FileName);
			}

			// 压缩设置
			fileContent.Add(new StringContent(compressImages.ToString()), Config.CompressFormKey);

			// DefaultSubPath
			fileContent.Add(new StringContent(subPath ?? string.Empty), Config.SubPathKey);

			var response = await httpClient.PostAsync(Config.ApiUri, fileContent, cancellationToken);
			response.EnsureSuccessStatusCode();

			return JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
		}
	}

	/// <summary>
	/// 向服务器上传一个或多个文件。使用默认压缩设置。
	/// </summary>
	/// <param name="files">要上传的文件的集合。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含上传后实际可访问的文件的路径。</returns>
	public Task<IEnumerable<string>> UploadAsync(IEnumerable<UploadFileInfo> files,
		CancellationToken cancellationToken = default) =>
		UploadAsync(files, Config.DefaultSubPath, Config.CompressByDefault, cancellationToken);
}