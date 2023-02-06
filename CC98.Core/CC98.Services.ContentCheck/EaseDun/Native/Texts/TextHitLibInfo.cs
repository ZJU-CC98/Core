using System;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     表示一个自定义库命中信息。
/// </summary>
public class TextHitLibInfo : IHitLibInfo
{
	/// <summary>
	///     命中的库类型。
	/// </summary>
	public required HitLibType Type { get; set; }

	/// <summary>
	///     命中的库的内容。
	/// </summary>
	public required string Entity { get; set; }

	/// <summary>
	///     命中的库的解封时间。以 Linux 毫秒时间戳为单位。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public required DateTimeOffset ReleaseTime { get; set; }
}