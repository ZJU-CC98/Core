using System.IO;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     表示单个需要检测的图片的数据。
/// </summary>
public class ImageCheckFileInfo
{
	/// <summary>
	///     图片的文件名。
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     图片的内容流。注意本方法不会自动关闭流。
	/// </summary>
	public required Stream Content { get; set; }

	/// <summary>
	///     图片的标识。
	/// </summary>
	public required string Id { get; set; }
}