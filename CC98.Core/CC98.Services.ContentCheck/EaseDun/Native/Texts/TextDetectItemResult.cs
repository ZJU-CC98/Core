namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     定义文本查询的单个内容结果。
/// </summary>
public class TextDetectItemResult
{
	/// <summary>
	///     文本内容安全检测结果。
	/// </summary>
	public required TextAntiSpamInfo AntiSpam { get; set; }
}