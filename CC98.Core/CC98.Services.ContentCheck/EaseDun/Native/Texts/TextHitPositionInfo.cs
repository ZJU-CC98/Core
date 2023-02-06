using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     定义一个命中的位置信息。
/// </summary>
public class TextHitPositionInfo
{
	/// <summary>
	///     命中的位置类型。
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("fieldName")]
	public required HitPositionField Field { get; set; }

	/// <summary>
	///     命中的开始位置。
	/// </summary>
	[JsonPropertyName("startPos")]
	public required int StartPosition { get; set; }

	/// <summary>
	///     命中的结束位置。
	/// </summary>
	[JsonPropertyName("endPos")]
	public required int EndPosition { get; set; }
}