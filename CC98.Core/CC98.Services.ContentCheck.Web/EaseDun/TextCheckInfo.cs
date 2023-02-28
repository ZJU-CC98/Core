using System;
using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     表示文本检测的必须数据。
/// </summary>
public class TextCheckInfo
{
	/// <summary>
	/// 表示要检测的一项或多项文本数据的集合。
	/// </summary>
	public required TextCheckItemInfo[] Items { get; set; }

	/// <summary>
	///     检测的分类。
	/// </summary>
	[Required]
	public required int[] CheckTypes { get; set; } = Array.Empty<int>();
}