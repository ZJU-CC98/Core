namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     表示文本命中的线索信息。
/// </summary>
public class TextHitInfo : IHitInfo
{
	/// <summary>
	///     命中的线索内容。
	/// </summary>
	public required string Value { get; set; }

	/// <summary>
	///     该线索所有命中位置的集合。
	/// </summary>
	public required TextHitPositionInfo[] Positions { get; set; }
}