namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
/// 表示单次命中的子标签。
/// </summary>
public interface IHitSubLabel
{
	/// <summary>
	///     结果命中的子标签标识。
	/// </summary>
	string SubLabel { get; }

	/// <summary>
	/// 子标签的置信度。如果该属性为 <c>null</c> 则表示未计算置信度。
	/// </summary>
	public double? Rate { get; }

	/// <summary>
	///     结果命中的详细信息。
	/// </summary>
	IHitSubLabelDetailInfo? Details { get; }
}