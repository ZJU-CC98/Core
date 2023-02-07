namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示图片审核命中的原因。
/// </summary>
public enum ImageHitStrategy
{
	/// <summary>
	///     图片内容命中策略。
	/// </summary>
	Image,

	/// <summary>
	///     图片上的文字命中策略。
	/// </summary>
	Text
}