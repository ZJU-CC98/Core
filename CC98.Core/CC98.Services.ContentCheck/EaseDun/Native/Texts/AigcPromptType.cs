namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 定义 AI 生成内容的引导类型。
/// </summary>
public enum AigcPromptType
{
	/// <summary>
	/// 未定义引导类型。
	/// </summary>
	Undefined = 0,
	/// <summary>
	/// 内容需拦截。
	/// </summary>
	InterceptionRequired = 1,
	/// <summary>
	/// 内容需正向引导。
	/// </summary>
	PositiveGuidanceRequired = 2,
	/// <summary>
	/// 正确答案。
	/// </summary>
	CorrectAnswer = 3,
	/// <summary>
	/// 内容需拦截或正向引导。
	/// </summary>
	InterceptionOrPositiveGuidanceRequired = 4,
	/// <summary>
	/// 正常问题
	/// </summary>
	NormalQuestion = 5
}