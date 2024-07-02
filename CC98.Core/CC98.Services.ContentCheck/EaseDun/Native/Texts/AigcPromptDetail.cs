namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 表示 AI 生成内容的详情。
/// </summary>
public class AigcPromptDetail
{
	/// <summary>
	/// 获取或设置 AI 生成内容的提示类型。
	/// </summary>
	public AigcPromptType Type { get; set; }

	/// <summary>
	/// 获取或设置 AI 生成内容的回答。
	/// </summary>
	public string? Answer { get; set; }
}