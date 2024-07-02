using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CC98.Services.ContentCheck.EaseDun.Native.Video;

/// <summary>
/// 定义视频检测服务的请求主体。
/// </summary>
public class VideoDetectRequestBody : CommonRequestBody
{
	/// <summary>
	/// V4 业务接口版本号的字符串。该字段为常量。
	/// </summary>
	public const string VersionV4 = "v4";

	/// <summary>
	/// 业务接口版本号。目前为 v4。
	/// </summary>
	[StringLength(4)]
	public required string Version { get; set; }

	/// <summary>
	/// 视频的下载地址。
	/// </summary>
	[Url]
	[StringLength(2048)]
	public required string Url { get; set; }

	/// <summary>
	/// 视频的标题。
	/// </summary>
	[StringLength(512)]
	public string? Title { get; set; }

	/// <summary>
	/// 数据回调参数。在产生检测结果后，该参数将原样包含在结果中。
	/// </summary>
	[StringLength(512)]
	public string? Callback { get; set; }

	/// <summary>
	/// 用于将结果通知客户的回调 URL。
	/// </summary>
	[Url]
	[StringLength(512)]
	public string? CallbackUrl { get; set; }


	/// <summary>
	/// 要截取帧的时间间隔，以秒为单位。默认为 5 秒截取一次。
	/// </summary>
	[Range(0.5, 600)]
	[JsonPropertyName("scFrequency")]
	public double? CheckFrameInterval { get; set; }

	/// <summary>
	/// 截取帧的数量，设定该参数时，自动截取首尾帧，其它帧时间按照截帧数量平均分布，而 <see cref="CheckFrameInterval"/> 设置将被忽略。
	/// </summary>
	[Range(0, 50000)]
	public int? CheckFrameCount { get; set; }

	/// <summary>
	/// 自定义额外扩展参数。
	/// </summary>
	[StringLength(35000)]
	public string? Extension { get; set; }

	/// <summary>
	/// 业务结算标识，用于资源账单分类统计。
	/// </summary>
	[StringLength(32)]
	public string? SubProduct { get; set; }

	/// <summary>
	/// 用于区分不同视频的标识。如两个视频该字段相同，则将被视为同一个视频，不会重复检测。如果不提供该字段，则使用 <see cref="Url"/> 作为标识。
	/// </summary>
	[StringLength(256)]
	public string? UniqueKey { get; set; }
}
