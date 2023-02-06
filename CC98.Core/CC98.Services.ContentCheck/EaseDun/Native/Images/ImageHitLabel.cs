using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
/// 定义图像安全检测命中的标签信息。
/// </summary>
public class ImageHitLabel : IHitLabel
{
	/// <summary>
	///     命中的标签类型。
	/// </summary>
	public required int Label { get; set; }

	/// <summary>
	///     置信度数值，为 0-1 区间小数。1 为最高置信度，0 为最低置信度。
	/// </summary>
	public required double Rate { get; set; }

	/// <summary>
	///     本标签的结果命中类型。
	/// </summary>
	public required ItemResultSuggestion Level { get; set; }

	/// <summary>
	///     本次命中的详细子分类信息。
	/// </summary>
	public ImageHitSubLabel[]? SubLabels { get; set; }

	/// <inheritdoc />
	IEnumerable<IHitSubLabel>? IHitLabel.SubLabels => SubLabels;
}