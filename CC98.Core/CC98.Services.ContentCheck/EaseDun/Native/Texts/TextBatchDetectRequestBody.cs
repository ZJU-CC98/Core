using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using Sakura.Text.Json.JsonFlattener.Core;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     文本批量检测的请求主体。
/// </summary>
[PublicAPI]
public class TextBatchDetectRequestBody : TextDetectRequestBodyBase
{
	/// <summary>
	///     要检测的内容的集合。
	/// </summary>
	/// <remarks>
	///     目前 API 限制单次检测最多 100 条数据。
	/// </remarks>
	[MaxLength(100)]
	public required TextItemWithExtendInfo[] Texts { get; set; }
}

/// <summary>
/// 为 <see cref="TextItem"/> 和 <see cref="RequestExtendedInfo"/> 提供封装容器。
/// </summary>
public partial class TextItemWithExtendInfo
{
	/// <summary>
	///     获取或设置要检测的项目的信息。
	/// </summary>
	[JsonFlatten]
	[JsonIgnore]
	public TextItem Item { get; set; } = null!;

	/// <summary>
	/// 请求的扩展参数。
	/// </summary>
	[JsonFlatten]
	[JsonIgnore]
	public RequestExtendedInfo ExtendedInfo { get; set; } = new();
}

