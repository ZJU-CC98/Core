using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 定义和用户隐私相关的项目。
/// </summary>
public enum UserPrivacyInfo
{
	/// <summary>
	/// 用户标识。
	/// </summary>
	[Display(Name = "用户编号")]
	Id,
	/// <summary>
	/// 用户名。
	/// </summary>
	[Display(Name = "用户名")]
	Name,
	/// <summary>
	/// 年龄。
	/// </summary>
	[Display(Name = "年龄")]
	Age,
	/// <summary>
	/// 性别。
	/// </summary>
	[Display(Name = "性别")]
	Gender,
	/// <summary>
	/// 用户电话。
	/// </summary>
	[Display(Name = "电话号码")]
	PhoneNumber,
	/// <summary>
	/// 注册时间。
	/// </summary>
	[Display(Name = "注册时间")]
	RegisterTime,
	/// <summary>
	/// 粉丝数。
	/// </summary>
	[Display(Name = "粉丝数")]
	FanCount,
	/// <summary>
	/// 认证标识。
	/// </summary>
	[Display(Name = "通行证")]
	SchoolId,
}