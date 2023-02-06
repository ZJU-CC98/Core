using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示图像命中的详细信息。
/// </summary>
public class ImageHitSubLabelDetailInfo : IHitSubLabelDetailInfo
{
	/// <summary>
	///     命中的关键词的列表。
	/// </summary>
	public ImageHitKeywordInfo[]? Keywords { get; set; }

	/// <summary>
	///     命中的自定义库的列表。
	/// </summary>
	public ImageHitLibInfo[]? LibInfos { get; set; }

	/// <summary>
	///     命中的反作弊信息。
	/// </summary>
	public HitAntiCheatInfo? AntiCheat { get; set; }

	/// <summary>
	///     命中的线索信息。
	/// </summary>
	public ImageHitInfo[]? HitInfos { get; set; }

	/// <inheritdoc />
	IEnumerable<IHitKeywordInfo>? IHitSubLabelDetailInfo.Keywords => Keywords;
	/// <inheritdoc />
	IEnumerable<IHitLibInfo>? IHitSubLabelDetailInfo.LibInfos => LibInfos;
	/// <inheritdoc />
	IEnumerable<IHitInfo>? IHitSubLabelDetailInfo.HitInfos => HitInfos;
}