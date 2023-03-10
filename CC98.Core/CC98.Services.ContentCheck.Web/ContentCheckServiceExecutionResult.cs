using CC98.Services.ContentCheck.Data;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 表示内容审查服务的运行结果。
/// </summary>
public class ContentCheckServiceExecutionResult
{
	/// <summary>
	/// 内容审查的最终判定结果。
	/// </summary>
	public ContentCheckResultType Result { get; set; }

	/// <summary>
	/// 内容审查服务发出的请求数据。
	/// </summary>
	public string? Request { get; set; }

	/// <summary>
	/// 内容审查服务获得的响应数据。
	/// </summary>
	public string? Response { get; set; }

}