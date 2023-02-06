using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
/// 表示一个命中的标签。
/// </summary>
public interface IHitLabel
{
	/// <summary>
	///     命中的标签类型。
	/// </summary>
	int Label { get; }

	/// <summary>
	///     置信度数值，为 0-1 区间小数。1 为最高置信度，0 为最低置信度。
	/// </summary>
	double Rate { get; }

	/// <summary>
	///     本标签的结果命中类型。
	/// </summary>
	ItemResultSuggestion Level { get; }

	/// <summary>
	///     本次命中的详细子分类信息。
	/// </summary>
	IEnumerable<IHitSubLabel>? SubLabels { get; }
}