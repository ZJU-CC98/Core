using System.Diagnostics.CodeAnalysis;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 提供 <see cref="ZuaaTokenServiceOptions"/> 相关选项的默认值。该类型为静态类型。
/// </summary>
public static class ZuaaTokenServiceDefaults
{
	/// <summary>
	/// 为 <see cref="ZuaaTokenServiceOptions.BaseUri"/> 提供默认值。
	/// </summary>
	[StringSyntax(StringSyntaxAttribute.Uri)]
	public const string BaseUri = "https://zuash.zju.edu.cn/hall/wp/ccne/";
}