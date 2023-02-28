using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 定义对单类内容的检查模式。
/// </summary>
public enum ContentCheckTypeMode
{
	/// <summary>
	/// 不检查此类内容。
	/// </summary>
	[Display(Name = "不执行任何审查")]
	Disabled = 0,
	/// <summary>
	/// 对此类内容进行完全检查。
	/// </summary>
	[Display(Name = "执行完全审查")]
	All,
	/// <summary>
	/// 对此类内容使用自定义设置进行检查。
	/// </summary>
	[Display(Name = "自定义设置")]
	Custom,
}