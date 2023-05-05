using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using Sakura.Text.Json.JsonFlattener.Core;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     表示单个要检查的文本对象。
/// </summary>
[PublicAPI]
public partial class TextItem
{
	/// <summary>
	///     数据标识。用于在反馈结果中标识数据。
	/// </summary>
	[StringLength(128)]
	public required string DataId { get; set; }

	/// <summary>
	///     要检测的内容。
	/// </summary>
	[StringLength(10000)]
	public required string Content { get; set; }

	/// <summary>
	///     内容的标题。对于有标题的内容建议一并提供。
	/// </summary>
	[StringLength(512)]
	public string? Title { get; set; }

	/// <summary>
	///     数据类型。可用于后台分类筛选检查记录。文本检测服务自身不使用该字段。
	/// </summary>
	public int? DataType { get; set; }

	/// <summary>
	///     回调数据。在回调和轮询模式中会将其直接返回。
	/// </summary>
	/// <remarks>
	///     <see cref="DataId" /> 可作为定位数据位置的唯一标识（如发言
	///     ID），但数据内容本身可被用户多次修改。当要区分同一内容多次修改产生的多次检测请求时，可通过该属性记录详细信息（如本次请求的时间、原始内容等等、操作用户名和 IP 地址等等）。
	/// </remarks>
	public string? Callback { get; set; }

	/// <summary>
	///     发送时间。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public DateTimeOffset PublishTime { get; set; }

	/// <summary>
	///     回调 URL。使用回调模式时使用。目前要求响应时间不超过 2s。
	/// </summary>
	[StringLength(256)]
	[DataType(System.ComponentModel.DataAnnotations.DataType.Url)]
	public string? CallbackUrl { get; set; }

	/// <summary>
	///     数据来源渠道。可在该字段记录应用名称。
	/// </summary>
	[StringLength(128)]
	public string? Category { get; set; }

	/// <summary>
	///     业务结算 ID。如使用该字段，则后台将根据该字段分类统计请求的数量、金额等信息。
	/// </summary>
	[StringLength(32)]
	public string? SubProduct { get; set; }

	/// <summary>
	///     用于调用易盾智能风控服务的令牌。
	/// </summary>
	[StringLength(256)]
	public string? RiskControlToken { get; set; }

	/// <summary>
	///     易盾智能风控服务的业务标识。
	/// </summary>
	[StringLength(256)]
	public string? RiskControlBusinessId { get; set; }
}