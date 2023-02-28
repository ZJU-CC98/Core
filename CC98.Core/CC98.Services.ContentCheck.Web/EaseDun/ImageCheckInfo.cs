using System;
using System.ComponentModel.DataAnnotations;
using CC98.Services.ContentCheck.EaseDun.Native;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 包含所有图片检测请求的必须数据。
/// </summary>
public class ImageCheckInfo
{
	/// <summary>
	/// 要检查的类型。
	/// </summary>
	[Required]
	public required int[] CheckTypes { get; set; }

	/// <summary>
	/// 要检查的一个或多个文件。
	/// </summary>
	public required ImageCheckFileInfo[] Files { get; set; }


	#region 辅助信息

	/// <summary>
	/// 用户的昵称。
	/// </summary>
	public string? NickName { get; set; }

	/// <summary>
	/// 用户的设备标识。
	/// </summary>
	public string? DeviceId { get; set; }

	/// <summary>
	/// 用户的设备标识类型。
	/// </summary>
	public DeviceIdType? DeviceType { get; set; }

	/// <summary>
	/// 内容的发布时间。
	/// </summary>
	public DateTimeOffset? PublishTime { get; set; }

	/// <summary>
	/// 发布用户的账户名。
	/// </summary>
	public string? Account { get; set; }

	/// <summary>
	/// 发布使用的 IP 地址。
	/// </summary>
	public string? Ip { get; set; }

	#endregion
}