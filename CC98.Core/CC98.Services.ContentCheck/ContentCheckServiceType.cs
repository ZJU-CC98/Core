using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义内容审查服务的功能类型。
/// </summary>
public enum ContentCheckServiceType
{
	/// <summary>
	///     文本审查功能。
	/// </summary>
	[Display(Name = "文本")]
	Text,

	/// <summary>
	///     图像审查功能。
	/// </summary>
	[Display(Name = "图像")]
	Image,

	/// <summary>
	///     音频审查功能。
	/// </summary>
	[Display(Name = "音频")]
	Audio,

	/// <summary>
	///     视频审查功能。
	/// </summary>
	[Display(Name = "视频")]
	Video
}