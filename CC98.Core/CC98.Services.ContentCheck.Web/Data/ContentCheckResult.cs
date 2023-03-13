namespace CC98.Services.ContentCheck.Data;

/// <summary>
///     表示内容审核的结果。
/// </summary>
public enum ContentCheckResult
{
	/// <summary>
	///     审核通过。
	/// </summary>
	Pass,

	/// <summary>
	///     审核不通过。
	/// </summary>
	Failed,

	/// <summary>
	///     无法确定审核结果。
	/// </summary>
	Undetermined,

	/// <summary>
	///     审核过程中发生错误。
	/// </summary>
	Error
}