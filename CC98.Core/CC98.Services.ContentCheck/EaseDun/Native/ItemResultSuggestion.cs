namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示对内容的建议处置方式。
/// </summary>
public enum ItemResultSuggestion
{
	/// <summary>
	///     内容通过安全校验。
	/// </summary>
	Pass = 0,

	/// <summary>
	///     内容存在风险嫌疑。
	/// </summary>
	Suspect = 1,

	/// <summary>
	///     内容无法通过安全校验。
	/// </summary>
	Fail = 2
}