using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 定义对无法判断是否匿名的操作的处理方式。
/// </summary>
public enum AnonymousStateUnknownItemHandling
{
	/// <summary>
	/// 将无法判断的项目视为非匿名活动。
	/// </summary>
	[Display(Name = "视为非匿名操作")]
	AsNonAnonymous,
	/// <summary>
	/// 将无法判断的项目视为匿名活动。
	/// </summary>
	[Display(Name = "视为匿名操作")]
	AsAnonymous
}