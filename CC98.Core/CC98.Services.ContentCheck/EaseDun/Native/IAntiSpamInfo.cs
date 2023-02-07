using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示对内容的审查结果。
/// </summary>
public interface IAntiSpamInfo
{
	/// <summary>
	///     对结果的推荐处理操作。
	/// </summary>
	ItemResultSuggestion Suggestion { get; }

	/// <summary>
	///     结果为嫌疑类型时的嫌疑级别。未开通此功能时不返回该信息。
	/// </summary>
	ItemResultSuggestionLevel? SuggestionLevel { get; }

	/// <summary>
	///     结果产生的类型。
	/// </summary>
	ItemResultType ResultType { get; }

	/// <summary>
	///     内容的审查模式。
	/// </summary>
	ItemCensorType CensorType { get; }

	/// <summary>
	///     本次检测命中的所有标签。
	/// </summary>
	IEnumerable<IHitLabel> Labels { get; }
}