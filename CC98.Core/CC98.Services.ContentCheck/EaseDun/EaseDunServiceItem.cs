﻿using System;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     表示一项网易易盾内容服务的设置。
/// </summary>
public class EaseDunServiceItem
{
	/// <summary>
	///     服务的业务标识。
	/// </summary>
	public required string BusinessId { get; set; }

	/// <summary>
	///     该服务允许使用的检测标签。
	/// </summary>
	public required int[] ValidLabels { get; set; } = Array.Empty<int>();

	/// <summary>
	///     该服务实际目前启用的监测标签。
	/// </summary>
	public required int[] EnabledLabels { get; set; } = Array.Empty<int>();

	/// <summary>
	/// 该类型服务关联到的文件扩展名设置。
	/// </summary>
	public required string[] FileExtensions { get; set; } = Array.Empty<string>();
}