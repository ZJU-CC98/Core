namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     定义图片检测执行失败的原因。
/// </summary>
public enum ImageFailureReason
{
	/// <summary>
	///     未知原因。该项目仅作为枚举默认值，不使用该字段。
	/// </summary>
	Unknown = 0,

	/// <summary>
	///     无法通过图片的地址下载图片。
	/// </summary>
	ImageDownloadFailed = 610,

	/// <summary>
	///     给定的地址或者数据不是有效的图片格式。
	/// </summary>
	ImageFormatError = 620,

	/// <summary>
	///     其它原因。
	/// </summary>
	Other = 630
}