using System;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示对命中数据库的的命中信息。
/// </summary>
public interface IHitLibInfo
{
	/// <summary>
	///     命中的库类型。
	/// </summary>
	HitLibType Type { get; }

	/// <summary>
	///     命中的库对应的图片的原始 URL。
	/// </summary>
	string Entity { get; }

	/// <summary>
	///     命中的库的解封时间。以 Linux 毫秒时间戳为单位。
	/// </summary>

	DateTimeOffset ReleaseTime { get; }
}