using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     图片检测的请求主体。
/// </summary>
[PublicAPI]
public class ImageDetectRequestBody : CommonRequestBody
{
	/// <summary>
	///     该请求主体的数据版本。目前必须为 <see cref="ImageDetectSupportedVersions.V5_1" />。
	/// </summary>
	public string Version { get; set; } = ImageDetectSupportedVersions.V5_1;

	/// <summary>
	///     该请求的子数据类型。该数据仅用于请求分类管理使用，无实际意义。如留空则不使用此功能。
	/// </summary>
	public int? DataType { get; set; }

	/// <summary>
	///     该内容的发表时间。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public DateTimeOffset? PublishTime { get; set; }

	/// <summary>
	///     要检测的分类。
	/// </summary>
	[StringLength(512)]
	public string? CheckLabels { get; set; }

	/// <summary>
	///     要检测的一个或多个图片的内容。
	/// </summary>
	public required ImageItem[] Images { get; set; }

	/// <summary>
	///     业务计算标识。用于服务租赁方分类统计账单使用。
	/// </summary>
	[StringLength(32)]
	public string? SubProduct { get; set; }

	/// <summary>
	///     反作弊专用字段。启用反作弊服务时使用。
	/// </summary>
	[StringLength(256)]
	public string? Token { get; set; }
}