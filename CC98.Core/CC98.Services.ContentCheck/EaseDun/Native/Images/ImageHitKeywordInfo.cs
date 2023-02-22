namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示图像检测中的关键字检测结果。
/// </summary>
public class ImageHitKeywordInfo : IHitKeywordInfo
{
	/// <summary>
	///     命中内容的左上角 X 坐标。
	/// </summary>
	public double? X1 { get; set; }

	/// <summary>
	///     命中内容的左上角 Y 坐标。
	/// </summary>
	public double? Y1 { get; set; }

	/// <summary>
	///     命中内容的右下角 X 坐标。
	/// </summary>
	public double? X2 { get; set; }

	/// <summary>
	///     命中内容的右下角 Y 坐标。
	/// </summary>
	public double? Y2 { get; set; }

	/// <summary>
	///     命中的关键词的内容。
	/// </summary>
	public required string Word { get; set; }
}