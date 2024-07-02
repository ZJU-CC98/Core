using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     定义文本内容安全检测结果。
/// </summary>
public class TextAntiSpamInfo : ResponseItemBase, IAntiSpamInfo
{
	/// <summary>
	///     本次结果涉及到的不同策略版本信息。在跨版本比较中，可用于比较不同版本的效果。未进行跨版本比较时该属性为 <c>null</c>。
	/// </summary>
	public StrategyVersion[]? StrategyVersions { get; set; }

	/// <summary>
	///     是否为关联命中。
	/// </summary>
	public required bool IsRelatedHit { get; set; }

	/// <summary>
	///     本次检测命中的所有标签。
	/// </summary>
	public required TextHitLabel[] Labels { get; set; }

	/// <summary>
	/// 本次命中的首要标签类型。
	/// </summary>
	public required int Label { get; set; }

	/// <summary>
	/// 命中的二级标签分类名称。
	/// </summary>
	public string? SecondLabel { get; set; }

	/// <summary>
	/// 命中的三级标签分类名称。
	/// </summary>
	public string? ThirdLabel { get; set; }

	/// <summary>
	/// 自定义动作结果。
	/// </summary>
	public CustomActionType? CustomAction { get; set; }

	/// <summary>
	///     对结果的推荐处理操作。
	/// </summary>
	public required ItemResultSuggestion Suggestion { get; set; }

	/// <summary>
	///     结果为嫌疑类型时的嫌疑级别。未开通此功能时不返回该信息。
	/// </summary>
	public ItemResultSuggestionLevel? SuggestionLevel { get; set; }

	/// <summary>
	///     结果产生的类型。
	/// </summary>
	public required ItemResultType ResultType { get; set; }

	/// <summary>
	///     内容的审查模式。
	/// </summary>
	public required ItemCensorType CensorType { get; set; }

	/// <summary>
	///     对 <see cref="IAntiSpamInfo.Labels" /> 的实现。
	/// </summary>
	IEnumerable<IHitLabel> IAntiSpamInfo.Labels => Labels;
}