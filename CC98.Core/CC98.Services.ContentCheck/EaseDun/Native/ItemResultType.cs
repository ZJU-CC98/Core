namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     内容的结果处理类型。
/// </summary>
public enum ItemResultType
{
	/// <summary>
	///     未定义。不使用该值。
	/// </summary>
	None,

	/// <summary>
	///     机器自动审核结果。
	/// </summary>
	Machine,

	/// <summary>
	///     人工审核结果。
	/// </summary>
	Human
}