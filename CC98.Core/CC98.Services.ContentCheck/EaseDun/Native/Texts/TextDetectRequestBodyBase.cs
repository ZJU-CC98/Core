﻿using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     为 <see cref="TextSingleDetectRequestBody" /> 和 <see cref="TextBatchDetectRequestBody" /> 提供公共基础类型。
/// </summary>
public abstract class TextDetectRequestBodyBase : CommonRequestBody
{
	/// <summary>
	///     请求主体的格式版本。
	/// </summary>
	public string Version { get; set; } = TextDetectSupportVersions.V5_2;

	/// <summary>
	///     检查的类型，以逗号分割的编号字符串。
	/// </summary>
	public string? CheckLabels { get; set; }

	/// <summary>
	/// 业务对应的策略组标识，多个组可用英文逗号分割，最多支持 20 个策略组。
	/// </summary>
	[StringLength(512)]
	public string? CheckStrategyGroupIds { get; set; }

	/// <summary>
	///     SDK 提供的 Token。
	/// </summary>
	[StringLength(256)]
	public string? Token { get; set; }
}