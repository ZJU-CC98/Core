using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
/// 图片检测的请求主体。
/// </summary>
[PublicAPI]
public class ImageDetectRequestBody : CommonRequestBody
{
	/// <summary>
	/// 该请求主体的数据版本。目前必须为 <see cref="ImageDetectSupportedVersions.V5_1"/>。
	/// </summary>
	public string Version { get; set; } = ImageDetectSupportedVersions.V5_1;

	/// <summary>
	/// 该请求的子数据类型。该数据仅用于请求分类管理使用，无实际意义。
	/// </summary>
	public int DataType { get; set; }

	/// <summary>
	/// 该内容的发表时间。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public DateTimeOffset PublishTime { get; set; }

	/// <summary>
	/// 要检测的分类。
	/// </summary>
	[StringLength(512)]
	public string? CheckLabels { get; set; }

	/// <summary>
	/// 要检测的一个或多个图片的内容。
	/// </summary>
	public required ImageItem[] Images { get; set; }

	/// <summary>
	/// 设备的 IP 地址。用于增强审核准确度。
	/// </summary>
	[StringLength(128)]
	public string? Ip { get; set; }

	/// <summary>
	/// 设备的账号名称。用于匹配用户画像库，增强审核准确度。
	/// </summary>
	[StringLength(128)]
	public string? Account { get; set; }

	/// <summary>
	/// 用户的昵称。用于增强审核准确度。
	/// </summary>
	[JsonPropertyName("nickname")]
	[StringLength(128)]
	public string? NickName { get; set; }

	/// <summary>
	/// 用户的设备 ID。可用于增强准确度。支持明文或 MD5 加密，请使用大写字母存储。
	/// </summary>
	[StringLength(128)]
	public string? DeviceId { get; set; }

	/// <summary>
	/// <see cref="DeviceId"/> 对应的值的类型。
	/// </summary>
	public DeviceIdType? DeviceType { get; set; }

	/// <summary>
	/// 业务计算标识。用于服务租赁方分类统计账单使用。
	/// </summary>
	[StringLength(32)]
	public string? SubProduct { get; set; }

	/// <summary>
	/// 反作弊专用字段。启用反作弊服务时使用。
	/// </summary>
	[StringLength(256)]
	public string? Token { get; set; }

}