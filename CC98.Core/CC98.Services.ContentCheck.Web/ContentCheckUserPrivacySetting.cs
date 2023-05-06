using System;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 表示内容审核的隐私设置。
/// </summary>
public class ContentCheckUserPrivacySetting
{
	/// <summary>
	/// 审查时允许提供的用户隐私信息。
	/// </summary>
	public UserPrivacyInfo[] EnabledPrivacyInfos { get; set; } = Array.Empty<UserPrivacyInfo>();

	/// <summary>
	/// 对于匿名操作的项目是否也包含隐私信息。
	/// </summary>
	public required bool IncludeAnonymousItems { get; set; } = false;

	/// <summary>
	/// 对于无法判断匿名的项目如何进行处置。
	/// </summary>
	public required AnonymousStateUnknownItemHandling AnonymousStateUnknownItemHandling { get; set; } =
		AnonymousStateUnknownItemHandling.AsAnonymous;
}