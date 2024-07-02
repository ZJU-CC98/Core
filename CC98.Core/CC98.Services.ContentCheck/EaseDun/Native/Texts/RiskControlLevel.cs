namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 定义风险控制的等级。
/// </summary>
public enum RiskControlLevel
{
	/// <summary>
	/// 正常。
	/// </summary>
	Normal = 0,
	/// <summary>
	/// 低风险。
	/// </summary>
	Low = 1,
	/// <summary>
	/// 中等风险。
	/// </summary>
	Medium = 2,
	/// <summary>
	/// 高风险。
	/// </summary>
	High = 3,
	/// <summary>
	/// 内容被定义为来自可信来源。
	/// </summary>
	Trusted = 4
}