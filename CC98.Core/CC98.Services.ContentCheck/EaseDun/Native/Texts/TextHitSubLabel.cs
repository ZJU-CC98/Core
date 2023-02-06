namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     表示一个结果命中的子标签。
/// </summary>
public class TextHitSubLabel : IHitSubLabel
{
	/// <summary>
	///     结果命中的子标签标识。
	/// </summary>
	public required string SubLabel { get; set; }

	/// <summary>
	///     结果命中的详细信息。
	/// </summary>
	public TextHitSubLabelDetailInfo? Details { get; set; }

	/// <inheritdoc />
	double? IHitSubLabel.Rate => null;

	/// <inheritdoc />
	IHitSubLabelDetailInfo? IHitSubLabel.Details => Details;
}