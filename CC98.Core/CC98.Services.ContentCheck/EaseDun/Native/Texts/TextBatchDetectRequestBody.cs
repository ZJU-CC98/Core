using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

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
	public required TextItem[] Texts { get; set; }
}