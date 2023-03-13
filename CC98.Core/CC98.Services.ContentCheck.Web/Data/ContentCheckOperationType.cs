using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
///     定义内容审核操作的触发类型。
/// </summary>
public enum ContentCheckOperationType
{
	/// <summary>
	///     系统自动审核。
	/// </summary>
	[Display(Name = "自动")] Auto,

	/// <summary>
	///     人工审核。
	/// </summary>
	[Display(Name = "人工")] Manual
}