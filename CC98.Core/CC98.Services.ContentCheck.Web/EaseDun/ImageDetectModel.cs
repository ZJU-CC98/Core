using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace CC98.Management.Services.EaseDun;

public class ImageDetectModel
{
	/// <summary>
	/// 要检查的类型。
	/// </summary>
	[Required]
	public required int[] CheckTypes { get; set; } = Array.Empty<int>();

	/// <summary>
	/// 要检测的图片的集合。
	/// </summary>
	[Required]
	public required IFormFileCollection Files { get; set; }
}