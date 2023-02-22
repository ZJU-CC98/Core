using System;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示图像检测中的库命中结果。
/// </summary>
public class ImageHitLibInfo : IHitLibInfo
{
	/// <summary>
	///     针对该库目前命中的次数计数。
	/// </summary>
	public required int HitCount { get; set; }

	/// <summary>
	///     图中包含的可识别内容。
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	///     为 <see cref="Value" /> 设定的分组名称。
	/// </summary>
	public string? Group { get; set; }

	/// <summary>
	///     命中的库类型。目前只支持返回 <see cref="HitLibType.Image" />。
	/// </summary>
	public required HitLibType Type { get; set; }

	/// <summary>
	///     命中的库对应的图片的原始 URL。
	/// </summary>
	public required string Entity { get; set; }

	/// <summary>
	///     命中的库的解封时间。以 Linux 毫秒时间戳为单位。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public required DateTimeOffset ReleaseTime { get; set; }
}