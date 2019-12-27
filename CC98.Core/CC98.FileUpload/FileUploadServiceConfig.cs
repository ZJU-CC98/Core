using System.Collections.Generic;
using System.Collections.ObjectModel;
using IdentityModel;

namespace CC98.Services
{
	/// <summary>
	/// 提供文件上传服务的相关配置。
	/// </summary>
	public class FileUploadServiceConfig
	{
		/// <summary>
		/// 授权服务器地址。
		/// </summary>
		public string Authority { get; set; } = FileUploadServiceConfigDefaults.Authoriy;

		/// <summary>
		/// 客户端标识。
		/// </summary>
		public string ClientId { get; set; }

		/// <summary>
		/// 客户端密钥。
		/// </summary>
		public string ClientSecret { get; set; }

		/// <summary>
		/// 获取或设置一个值，指示是否使用 <see cref="OidcConstants.StandardScopes.OfflineAccess"/> 获取刷新令牌。
		/// </summary>
		public bool RequestOfflineAccess { get; set; }

		/// <summary>
		/// 需要申请的任何额外领域。
		/// </summary>
		public ICollection<string> AdditionalScopes { get; set; } = new Collection<string>();

		/// <summary>
		/// 上传文件的 API 地址。
		/// </summary>
		public string ApiUri { get; set; } = FileUploadServiceConfigDefaults.ApiUri;

		/// <summary>
		/// 上传文件时使用的表单名称。
		/// </summary>
		public string FormKey { get; set; } = FileUploadServiceConfigDefaults.FormKey;
	}

	/// <summary>
	/// 提供 <see cref="FileUploadService"/> 相关设置的默认值。该类型为静态类型。
	/// </summary>
	public static class FileUploadServiceConfigDefaults
	{
		/// <summary>
		/// <see cref="FileUploadService.Authority"/> 的默认值。该字段为常量。
		/// </summary>
		public const string Authoriy = "https://openid.cc98.org";
		/// <summary>
		/// <see cref="FileUploadService.ApiUri"/> 的默认值。该字段为常量。
		/// </summary>
		public const string ApiUri = "https://api.cc98.org/file/upload";
		/// <summary>
		/// <see cref="FileUploadService.FormKey"/> 的默认值。该字段为常量。
		/// </summary>
		public const string FormKey = "file";
	}
}