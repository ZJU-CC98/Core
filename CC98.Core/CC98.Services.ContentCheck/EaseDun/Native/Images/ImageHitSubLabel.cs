namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示一个结果命中的子标签。
/// </summary>
public class ImageHitSubLabel : IHitSubLabel
{
	/// <summary>
	///     结果命中的子标签标识。
	/// </summary>
	public required string SubLabel { get; set; }

	/// <summary>
	/// 图像命中的原因。
	/// </summary>
	public required ImageHitStrategy HitStrategy { get; set; }

	/// <summary>
	/// 本次命中的置信度。
	/// </summary>
	public required double Rate { get; set; }

	/// <summary>
	///     结果命中的详细信息。
	/// </summary>
	public ImageHitSubLabelDetailInfo? Details { get; set; }

	/// <inheritdoc />
	double? IHitSubLabel.Rate => Rate;

	/// <inheritdoc />
	IHitSubLabelDetailInfo? IHitSubLabel.Details => Details;
}