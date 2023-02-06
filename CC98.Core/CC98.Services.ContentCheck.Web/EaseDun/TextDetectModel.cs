using System;
using System.ComponentModel.DataAnnotations;

namespace CC98.Management.Services.EaseDun;

/// <summary>
///     表示检测的数据。
/// </summary>
public class TextDetectModel
{
	/// <summary>
	///     检测的内容。
	/// </summary>
	[Required]
	public required string Text { get; set; }

	/// <summary>
	///     检测的分类。
	/// </summary>
	[Required]
	public required int[] CheckTypes { get; set; } = Array.Empty<int>();
}