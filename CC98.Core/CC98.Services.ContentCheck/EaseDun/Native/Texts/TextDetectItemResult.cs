using CC98.Services.ContentCheck.EaseDun.Native.AntiCheat;

namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
///     定义文本查询的单个内容结果。
/// </summary>
public class TextDetectItemResult
{
	/// <summary>
	///     文本内容安全检测结果。
	/// </summary>
	public required TextAntiSpamInfo AntiSpam { get; set; }

	/// <summary>
	/// 文本反作弊检测结果。
	/// </summary>
	public SimpleResponseItemCollection<AntiCheatDetail>? AntiCheat { get; set; }

	/// <summary>
	/// 智能风控检测结果。
	/// </summary>
	public SimpleResponseItemCollection<RiskControlDetail>? RiskControl { get; set; }

	/// <summary>
	/// AI 生成内容提示分析结果。
	/// </summary>
	public SimpleResponseItemCollection<AigcPromptDetail>? AigcPrompt { get; set; }
}