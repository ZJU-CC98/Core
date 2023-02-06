namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     表示命中的关键词信息。
/// </summary>
public class TextHitKeywordInfo : IHitKeywordInfo
{
	/// <summary>
	///     命中的关键词的内容。
	/// </summary>
	public required string Word { get; set; }
}