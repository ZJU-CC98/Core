namespace CC98.Authentication.OpenIdConnect;

/// <summary>
/// 提供 CC98 身份验证的相关选项值。该类型的静态类型。
/// </summary>
public static class CC98Defaults
{
	/// <summary>
	/// 获取 CC98 身份验证服务的默认授权地址。该字段为常量。
	/// </summary>
	public const string Authority = "https://openid.cc98.org";

	/// <summary>
	/// 获取 CC98 身份验证服务的默认授权协议名称。该字段为常量。
	/// </summary>
	public const string AuthenticationScheme = "CC98";

	/// <summary>
	/// 获取 CC98 身份验证服务的默认显示名称。该字段为常量。
	/// </summary>
	public const string DisplayName = "CC98";
}