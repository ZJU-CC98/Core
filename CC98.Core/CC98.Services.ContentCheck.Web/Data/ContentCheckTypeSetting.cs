namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示针对单类内容的检查设置。
/// </summary>
public class ContentCheckTypeSetting
{
	/// <summary>
	/// 是否启用本类内容的检查设置。
	/// </summary>
	public ContentCheckTypeMode CheckMode { get; set; }

	/// <summary>
	/// 需要检查的内容的分类编号的集合。在 <see cref="CheckMode"/> 的值不为 <see cref="ContentCheckTypeMode.Custom"/> 时，该属性不产生任何作用。
	/// </summary>
	public int[]? Labels { get; set; }
}