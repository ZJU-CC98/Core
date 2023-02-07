namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     文本单次检测的请求主体。
/// </summary>
public class TextSingleDetectRequestBody : TextDetectRequestBodyBase
{
	/// <summary>
	///     获取或设置要检测的项目的信息。
	/// </summary>
	public required TextItem Item { get; set; }
}