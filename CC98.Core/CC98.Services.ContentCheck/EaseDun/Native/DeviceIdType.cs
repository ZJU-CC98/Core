using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     定义设备编号的类型。
/// </summary>
public enum DeviceIdType
{
	/// <summary>
	///     未定义类型。
	/// </summary>
	[Display(Name = "其它")]
	Other = 0,

	/// <summary>
	///     设备的 IMEI 编号。
	/// </summary>
	[Display(Name = "IMEI")]
	Imei = 10,

	/// <summary>
	///     设备的 AndroidID。
	/// </summary>
	[Display(Name = "AndroidID")]
	AndroidId = 11,

	/// <summary>
	///     设备的 IDFA 编号。
	/// </summary>
	[Display(Name = "IDFA")]
	Idfa = 12,

	/// <summary>
	///     设备的 IDFV 编号。
	/// </summary>
	[Display(Name = "IDFV")]
	Idfv = 13,

	/// <summary>
	///     设备的 MAC 地址。
	/// </summary>
	[Display(Name = "MAC 地址")]
	Mac = 14,

	/// <summary>
	///     设备的 IMEI 编号的 MD5 值。
	/// </summary>
	[Display(Name = "IMEI 的 MD5")]
	ImeiMd5 = 20,

	/// <summary>
	///     设备的 AndroidID 的 MD5 值。
	/// </summary>
	[Display(Name = "AndroidID 的 MD5")]
	AndroidIdMd5 = 21,

	/// <summary>
	///     设备的 IDFA 编号的 MD5 值。
	/// </summary>
	[Display(Name = "IDFA 的 MD5")]
	IdfaMd5 = 22,

	/// <summary>
	///     设备的 IDFV 编号的 MD5 值。
	/// </summary>
	[Display(Name = "IDFV 的 MD5")]
	IdfvMd5 = 23,

	/// <summary>
	///     设备的 MAC 编号的 MD5 值。
	/// </summary>
	[Display(Name = "MAC 地址的 MD5")]
	MacMd5 = 24
}