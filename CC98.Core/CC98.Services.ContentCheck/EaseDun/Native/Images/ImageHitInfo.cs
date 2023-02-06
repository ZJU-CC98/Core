namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
/// 表示图片命中的线索信息。
/// </summary>
public class ImageHitInfo : IHitInfo
{
	/// <summary>
	/// 命中的可识别内容。
	/// </summary>
	public required string Value { get; set; }
	/// <summary>
	/// 对 <see cref="Value"/> 设定的分组。
	/// </summary>
	public string? Group { get; set; }
	/// <summary>
	/// 命中位置的左上角的 X 坐标。
	/// </summary>
	public double? X1 { get; set; }
	/// <summary>
	/// 命中位置的左上角的 Y 坐标。
	/// </summary>
	public double? Y1 { get; set; }
	/// <summary>
	/// 命中位置的右下角的 X 坐标。
	/// </summary>
	public double? X2 { get; set; }
	/// <summary>
	/// 命中位置的右下角的 Y 坐标。
	/// </summary>
	public double? Y2 { get; set; }
}