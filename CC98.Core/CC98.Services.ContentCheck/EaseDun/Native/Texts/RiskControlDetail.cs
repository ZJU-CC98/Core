namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 风险控制的检测结果详情。
/// </summary>
public class RiskControlDetail
{
	/// <summary>
	/// 获取或设置风险的等级。若内容具有多种不同等级的风险，则返回最高值。
	/// </summary>
	public RiskControlLevel RiskLevel { get; set; }

	/// <summary>
	/// 获取或设置命中的风险标签的集合。
	/// </summary>
	public RiskControlHitInfo[]? HitInfos { get; set; }
}