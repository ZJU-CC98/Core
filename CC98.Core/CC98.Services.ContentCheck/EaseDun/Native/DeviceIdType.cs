namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     定义设备编号的类型。
/// </summary>
public enum DeviceIdType
{
	/// <summary>
	///     未定义类型。
	/// </summary>
	Other = 0,

	/// <summary>
	///     设备的 IMEI 编号。
	/// </summary>
	Imei = 10,

	/// <summary>
	///     设备的 AndroidID。
	/// </summary>
	AndroidId = 11,

	/// <summary>
	///     设备的 IDFA 编号。
	/// </summary>
	Idfa = 12,

	/// <summary>
	///     设备的 IDFV 编号。
	/// </summary>
	Idfv = 13,

	/// <summary>
	///     设备的 MAC 地址。
	/// </summary>
	Mac = 14,

	/// <summary>
	///     设备的 IMEI 编号的 MD5 值。
	/// </summary>
	ImeiMd5 = 20,

	/// <summary>
	///     设备的 AndroidID 的 MD5 值。
	/// </summary>
	AndroidIdMd5 = 21,

	/// <summary>
	///     设备的 IDFA 编号的 MD5 值。
	/// </summary>
	IdfaMd5 = 22,

	/// <summary>
	///     设备的 IDFV 编号的 MD5 值。
	/// </summary>
	IdfvMd5 = 23,

	/// <summary>
	///     设备的 MAC 编号的 MD5 值。
	/// </summary>
	MacMd5 = 24
}