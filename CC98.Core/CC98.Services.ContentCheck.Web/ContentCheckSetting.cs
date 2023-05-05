using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义论坛/版面的内容审查设置。
/// </summary>
public class ContentCheckSetting : IJsonOnDeserialized
{
	/// <summary>
	///     需要使用的审查服务提供程序。
	/// </summary>
	public required string ServiceProvider { get; set; }

	/// <summary>
	///     需要默认启用的审查内容和对应的分类设置。
	/// </summary>
	public Dictionary<ContentCheckServiceType, ContentCheckTypeSetting> CheckTypes { get; set; }
		= new()
		{
			[ContentCheckServiceType.Text] = new(),
			[ContentCheckServiceType.Image] = new(),
			[ContentCheckServiceType.Audio] = new(),
			[ContentCheckServiceType.Video] = new()
		};

	/// <summary>
	///     审查的默认记录级别。
	/// </summary>
	public required ContentCheckRecordLevel RecordLevel { get; set; }

	/// <summary>
	/// 审查的隐私设置。
	/// </summary>
	public required ContentCheckUserPrivacySetting PrivacySetting { get; set; }



	/// <inheritdoc />
	public void OnDeserialized()
	{
		CheckTypes.TryAdd(ContentCheckServiceType.Text, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Image, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Audio, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Video, new());
	}
}

/// <summary>
/// 表示内容审核的隐私设置。
/// </summary>
public class ContentCheckUserPrivacySetting
{
	/// <summary>
	/// 审查时允许提供的用户隐私信息。
	/// </summary>
	public UserPrivacyInfo[] EnabledPrivacyInfos { get; set; } = Array.Empty<UserPrivacyInfo>();

	/// <summary>
	/// 对于匿名操作的项目是否也包含隐私信息。
	/// </summary>
	public required bool IncludeAnonymousItems { get; set; } = false;

	/// <summary>
	/// 对于无法判断匿名的项目如何进行处置。
	/// </summary>
	public required AnonymousStateUnknownItemHandling AnonymousStateUnknownItemHandling { get; set; } =
		AnonymousStateUnknownItemHandling.AsAnonymous;
}

/// <summary>
/// 定义对无法判断是否匿名的操作的处理方式。
/// </summary>
public enum AnonymousStateUnknownItemHandling
{
	/// <summary>
	/// 将无法判断的项目视为非匿名活动。
	/// </summary>
	[Display(Name = "视为非匿名操作")]
	AsNonAnonymous,
	/// <summary>
	/// 将无法判断的项目视为匿名活动。
	/// </summary>
	[Display(Name = "视为匿名操作")]
	AsAnonymous
}

/// <summary>
/// 定义和用户隐私相关的项目。
/// </summary>
public enum UserPrivacyInfo
{
	/// <summary>
	/// 用户标识。
	/// </summary>
	Id,
	/// <summary>
	/// 用户名。
	/// </summary>
	Name,
	/// <summary>
	/// 年龄。
	/// </summary>
	Age,
	/// <summary>
	/// 性别。
	/// </summary>
	Gender,
	/// <summary>
	/// 用户电话。
	/// </summary>
	PhoneNumber,
	/// <summary>
	/// 注册时间。
	/// </summary>
	RegisterTime,
	/// <summary>
	/// 粉丝数。
	/// </summary>
	FanCount,
	/// <summary>
	/// 认证标识。
	/// </summary>
	SchoolId,
}