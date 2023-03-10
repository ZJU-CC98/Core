using System;
using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 包含所有图片检测请求的必须数据。
/// </summary>
public class ImageCheckInfo
{
	/// <summary>
	/// 要检查的类型。
	/// </summary>
	[Required]
	public required int[] CheckTypes { get; set; }

	/// <summary>
	/// 要检查的一个或多个文件。
	/// </summary>
	public required ImageCheckFileInfo[] Files { get; set; }

	/// <summary>
	/// 图片的发布时间。
	/// </summary>
	public required DateTimeOffset PublishTime { get; set; }
}