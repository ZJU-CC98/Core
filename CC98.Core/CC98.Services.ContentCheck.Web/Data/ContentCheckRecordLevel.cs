using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 定义内容审查模式使用的记录级别。
/// </summary>
public enum ContentCheckRecordLevel
{
	/// <summary>
	/// 记录所有审查操作。
	/// </summary>
	[Display(Name = "全部", Description = "记录全部审查操作")]
	All = 0,
	/// <summary>
	/// 记录审查结果至少为嫌疑级别的审查操作。
	/// </summary>
	[Display(Name = "嫌疑", Description = "记录结果至少为嫌疑级别的审查操作")]
	Suspect,
	/// <summary>
	/// 记录审查结果至少为危险级别的审查操作。
	/// </summary>
	[Display(Name = "危险", Description = "记录结果至少为危险级别的审查操作")]
	Fail
}