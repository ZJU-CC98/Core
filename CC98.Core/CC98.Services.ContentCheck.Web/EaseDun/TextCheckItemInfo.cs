using System;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 表示文本检测中的单项内容数据。
/// </summary>
public class TextCheckItemInfo
{
	/// <summary>
	/// 检测的内容的标题。
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	///     检测的内容。
	/// </summary>
	public required string Text { get; set; }

	/// <summary>
	/// 检测的数据的唯一标识。
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// 内容的发布时间。
	/// </summary>
	public required DateTimeOffset Time { get; set; }
}