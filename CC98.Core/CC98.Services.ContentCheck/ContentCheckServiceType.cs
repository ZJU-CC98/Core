namespace CC98.Services.ContentCheck;

/// <summary>
///     定义内容审查服务的功能类型。
/// </summary>
public enum ContentCheckServiceType
{
	/// <summary>
	///     文本审查功能。
	/// </summary>
	Text,

	/// <summary>
	///     图像审查功能。
	/// </summary>
	Image,

	/// <summary>
	///     音频审查功能。
	/// </summary>
	Audio,

	/// <summary>
	///     视频审查功能。
	/// </summary>
	Video
}