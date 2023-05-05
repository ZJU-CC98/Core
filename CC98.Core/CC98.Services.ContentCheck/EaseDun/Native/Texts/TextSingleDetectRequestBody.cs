using System.Text.Json.Serialization;

using Sakura.Text.Json.JsonFlattener.Core;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     文本单次检测的请求主体。
/// </summary>
public partial class TextSingleDetectRequestBody : TextDetectRequestBodyBase
{
	/// <summary>
	///     获取或设置要检测的项目的信息。
	/// </summary>
	[JsonFlatten]
	[JsonIgnore]
	public TextItem Item { get; set; } = null!;

	/// <summary>
	/// 请求的扩展参数。
	/// </summary>
	[JsonFlatten]
	[JsonIgnore]
	public RequestExtendedInfo ExtendedInfo { get; set; } = new();
}