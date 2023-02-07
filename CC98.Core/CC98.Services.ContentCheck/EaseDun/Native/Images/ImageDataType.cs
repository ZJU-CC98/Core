namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

public enum ImageDataType
{
	/// <summary>
	///     未定义，不使用该字段。
	/// </summary>
	NotDefined = 0,

	/// <summary>
	///     图片的数据为图片的 URL 地址。
	/// </summary>
	Url = 1,

	/// <summary>
	///     图片的数据为图片内容的 BASE64 字符串。
	/// </summary>
	Base64 = 2
}