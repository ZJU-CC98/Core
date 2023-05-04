using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     请求中可选的扩展参数信息。
/// </summary>
public class RequestExtendedInfo
{
	/// <summary>
	///     用户账户标识，用于匹配用户画像库。如同时接入反外挂系统，则需要和反外挂信息中的 roleAccount / user_account 信息保持一致。
	/// </summary>
	[StringLength(128)]
	[JsonPropertyName("account")]
	public string? Account { get; set; }

	/// <summary>
	///     用户的手机号码，用于风险库匹配。默认为国内手机号码。如为海外号码则须包含国家地区代码如 +447410xxx186 格式。也可提供手机号码的 MD5 散列值。
	/// </summary>
	[StringLength(64)]
	[JsonPropertyName("phone")]
	public string? Phone { get; set; }

	/// <summary>
	///     用户的昵称，用于增强审核准确度。
	/// </summary>
	[StringLength(128)]
	[JsonPropertyName("nickname")]
	public string? NickName { get; set; }

	/// <summary>
	///     用户性别。建议直播类业务提供该信息以增强准确度。
	/// </summary>
	[JsonPropertyName("gender")]
	public Gender? Gender { get; set; }

	/// <summary>
	///     用户年龄，如设置为 <c>0</c> 则表示年龄未知。用于增强审核准确度。
	/// </summary>
	[JsonPropertyName("age")]
	public int? Age { get; set; }

	/// <summary>
	///     用户的等级。
	/// </summary>
	[JsonPropertyName("level")]
	public UserLevel? Level { get; set; }

	/// <summary>
	///     用户的注册时间。用于增强审核准确度。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	[JsonPropertyName("registerTime")]
	public DateTimeOffset? RegisterTime { get; set; }

	/// <summary>
	///     用户的好友数。用于在社交、直播类场景中增强审核准确度。
	/// </summary>
	[JsonPropertyName("friendNum")]
	public int? FriendCount { get; set; }

	/// <summary>
	///     用户的粉丝数。用于在社交、直播类场景中增强审核准确度。
	/// </summary>
	[JsonPropertyName("fansNum")]
	public int? FanCount { get; set; }

	/// <summary>
	///     用户的特权等级。用于增强审核准确度。
	/// </summary>
	[JsonPropertyName("isPremiumUse")]
	public UserPremiumType? IsPremiumUse { get; set; }

	/// <summary>
	///     用户的角色，需和后台设置保持一致。审核系统后台可设定多种用户角色并分别设定不同的审核规则。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("role")]
	public string? Role { get; set; }

	/// <summary>
	///     用户的设备标识，用于匹配用户画像库。请使用大写字母形式记录。
	/// </summary>
	[StringLength(128)]
	[JsonPropertyName("deviceId")]
	public string? DeviceId { get; set; }

	/// <summary>
	///     用户的设备标识对应的含义。
	/// </summary>
	[JsonPropertyName("deviceType")]
	public DeviceIdType? DeviceType { get; set; }

	/// <summary>
	///     用户的 MAC 地址，用于匹配用户画像库。
	/// </summary>
	[StringLength(64)]
	[JsonPropertyName("mac")]
	public string? Mac { get; set; }

	/// <summary>
	///     用户设备的 IMEI 代码，用于匹配用户画像库。
	/// </summary>
	[StringLength(64)]
	[JsonPropertyName("imei")]
	public string? Imei { get; set; }

	/// <summary>
	///     用户设备的 IDFA 编码，用于匹配用户画像库。
	/// </summary>
	[StringLength(64)]
	[JsonPropertyName("idfa")]
	public string? Idfa { get; set; }

	/// <summary>
	///     用户设备的 IDFV 编码，用于匹配用户画像库。
	/// </summary>
	[StringLength(64)]
	[JsonPropertyName("idfv")]
	public string? Idfv { get; set; }

	/// <summary>
	///     业务应用的版本号。审核系统后台可对不同版本号设定各自的审核规则。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("appVersion")]
	public string? AppVersion { get; set; }

	/// <summary>
	///     当前用户发送的消息的接收者的标识，可在用户私聊、评论场景使用。后台可根据关联标识对双方用户内容进行关联检测，增强匹配准确度。
	/// </summary>
	[JsonPropertyName("receiveUid")]
	[StringLength(64)]
	public string? ReceiveUserId { get; set; }

	/// <summary>
	///     当前用户和消息接收者之间的关系。可在私聊、评论场景增强审核准确度。
	/// </summary>
	[JsonPropertyName("relationship")]
	public UserRelationship? Relationship { get; set; }

	/// <summary>
	///     群聊的标识。可在群聊场景使用，增强上下文间的审核准确度。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("groupId")]
	public string? GroupId { get; set; }

	/// <summary>
	///     房间标识。在聊天室、直播类业务场景中使用，后台可针对不同房间设定不同的审核规则，并可根据房间的内容上下文增强审核准确度。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("roomId")]
	public string? RoomId { get; set; }

	/// <summary>
	///     文章/帖子标识，可用于标记当前内容对应的文章，在发帖、回帖类场景中使用。后台可根据该标识对相关内容进行关联，增强审核准确度。
	/// </summary>
	[StringLength(128)]
	[JsonPropertyName("topic")]
	public string? Topic { get; set; }

	/// <summary>
	///     主评论标识，可用于标识当前内容对应的主评论，在针对评论进行引用、答复等业务场景中使用。后台可根据该标识对相关内容进行关联，增强审核准确度。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("commentId")]
	public string? CommentId { get; set; }

	/// <summary>
	///     商品标识。在直播卖货/商品介绍类场景中可使用。后台可根据不同商品设定不同的审核策略，增强审核准确度。
	/// </summary>
	[StringLength(32)]
	[JsonPropertyName("commodityId")]
	public string? CommodityId { get; set; }

	/// <summary>
	///     用户当前操作的 IP 地址。用于匹配画像库并增强审核准确度。
	/// </summary>
	[StringLength(128)]
	[JsonPropertyName("ip")]
	public string? Ip { get; set; }

	/// <summary>
	///     关联的内容标识。如本次内容关联到多个相关内容，可使用该参数设定所有关联的内容标识。标识间使用逗号分隔，目前最多支持同时提供 3 个关联标识。
	/// </summary>
	[StringLength(512)]
	[JsonPropertyName("relatedKeys")]
	public string? RelatedKeys { get; set; }

	/// <summary>
	///     自定义的扩展字符串参数。
	/// </summary>
	[JsonPropertyName("extStr1")]
	[StringLength(128)]

	public string? ExtString1 { get; set; }

	/// <summary>
	///     自定义的扩展字符串参数。
	/// </summary>
	[JsonPropertyName("extStr2")]
	[StringLength(128)]
	public string? ExtString2 { get; set; }

	/// <summary>
	///     自定义的扩展整数参数。
	/// </summary>
	[JsonPropertyName("extLon1")]
	public long? ExtNumber1 { get; set; }

	/// <summary>
	///     自定义的扩展整数参数。
	/// </summary>
	[JsonPropertyName("extLon2")]
	public long? ExtNumber2 { get; set; }
}