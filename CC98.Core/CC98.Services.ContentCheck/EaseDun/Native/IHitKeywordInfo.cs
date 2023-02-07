namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示命中的关键词的信息。
/// </summary>
public interface IHitKeywordInfo
{
	/// <summary>
	///     命中的关键词。
	/// </summary>
	string Word { get; }
}