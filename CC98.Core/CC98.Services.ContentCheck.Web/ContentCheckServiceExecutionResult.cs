namespace CC98.Services.ContentCheck;

/// <summary>
///     表示内容审查服务的运行结果。
/// </summary>
public class ContentCheckServiceExecutionResult
{
	/// <summary>
	///     执行的内容审核类型。
	/// </summary>
	public required ContentCheckServiceType ServiceType { get; set; }

	/// <summary>
	///     内容审查的最终判定结果。
	/// </summary>
	public required ContentCheckResult Result { get; set; }

	/// <summary>
	///     内容审查服务发出的请求数据。
	/// </summary>
	public required string? Request { get; set; }

	/// <summary>
	///     内容审查服务获得的响应数据。
	/// </summary>
	public required string? Response { get; set; }
}