namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     内容的审查模式。
/// </summary>
public enum ItemCensorType
{
	/// <summary>
	///     纯机器审查。
	/// </summary>
	AllMachine,

	/// <summary>
	///     机器审查+部分人工审查。
	/// </summary>
	PartialHuman,

	/// <summary>
	///     机器审查+完整人工审查。
	/// </summary>
	FullHuman
}