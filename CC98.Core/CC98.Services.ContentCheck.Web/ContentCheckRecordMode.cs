using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义内容审查服务执行时对结果的记录模式。
/// </summary>
public enum ContentCheckRecordMode
{
	/// <summary>
	///     使用默认记录模式。也即当结果严重性到达系统后台设置的严重性级别时，记录结果；否则不记录结果。
	/// </summary>
	[Display(Name = "使用系统默认设置", ShortName = "默认")]
	Default = 0,

	/// <summary>
	///     始终记录结果。
	/// </summary>
	[Display(Name = "始终记录审查结果", ShortName = "始终")]
	Always,

	/// <summary>
	///     始终不记录结果。
	/// </summary>
	[Display(Name = "从不记录检查结果", ShortName = "从不")]
	Never
}