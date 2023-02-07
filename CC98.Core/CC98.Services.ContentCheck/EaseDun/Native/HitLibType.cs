namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示命中的自定义库的类型。
/// </summary>
public enum HitLibType
{
	/// <summary>
	///     未定义。不使用该值。
	/// </summary>
	NotDefined = 0,

	/// <summary>
	///     自定义用户名单库。
	/// </summary>
	User,

	/// <summary>
	///     自定义 IP 名单库。
	/// </summary>
	Ip,

	/// <summary>
	///     自定义设备名单库。
	/// </summary>
	Device,

	/// <summary>
	///     自定义图片名单库。
	/// </summary>
	Image
}