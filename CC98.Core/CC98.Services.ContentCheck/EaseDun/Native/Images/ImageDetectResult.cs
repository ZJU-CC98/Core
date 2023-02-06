namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
/// 定义图片查询的单个内容结果。
/// </summary>
public class ImageDetectResult
{
	/// <summary>
	/// 图片内容安全检测结果。
	/// </summary>
	public required ImageAntiSpamInfo AntiSpam { get; set; }
}