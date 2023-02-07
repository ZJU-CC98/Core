using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     表示子标签命中的详细信息。
/// </summary>
public interface IHitSubLabelDetailInfo
{
	/// <summary>
	///     命中的关键词的列表。
	/// </summary>
	IEnumerable<IHitKeywordInfo>? Keywords { get; }

	/// <summary>
	///     命中的自定义库的列表。
	/// </summary>
	IEnumerable<IHitLibInfo>? LibInfos { get; }

	/// <summary>
	///     命中的反作弊信息。
	/// </summary>
	HitAntiCheatInfo? AntiCheat { get; }

	/// <summary>
	///     命中的线索信息。
	/// </summary>
	IEnumerable<IHitInfo>? HitInfos { get; }
}