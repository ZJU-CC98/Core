using System.Diagnostics.CodeAnalysis;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 提供 <see cref="ZuaaTokenService"/> 的相关配置选项。
/// </summary>
public class ZuaaTokenServiceOptions
{
	/// <summary>
	/// 浙大校友身份令牌验证服务的基础路径。
	/// </summary>
	[StringSyntax(StringSyntaxAttribute.Uri)]
	public required string BaseUri { get; set; } = ZuaaTokenServiceDefaults.BaseUri;

	/// <summary>
	/// 创建 <see cref="HttpClient"/> 时使用的配置名称。该属性默认值为 <c>null</c>。
	/// </summary>
	public string? HttpClientName { get; set; }
}