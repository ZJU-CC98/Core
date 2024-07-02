namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 表示风险控制的命中信息。
/// </summary>
public class RiskControlHitInfo
{
	/// <summary>
	/// 命中的风险标签编号。
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// 命中的风险标签对应的名称。多个级别的风险使用横线（-）依次连接。
	/// </summary>
	public required string Name { get; set; }
}