namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     定义一个策略版本。
/// </summary>
public class StrategyVersion
{
	/// <summary>
	///     该策略版本关联的标签。
	/// </summary>
	public required CheckLabel Label { get; set; }

	/// <summary>
	///     该策略版本关联的版本号。
	/// </summary>
	public required string Version { get; set; }
}