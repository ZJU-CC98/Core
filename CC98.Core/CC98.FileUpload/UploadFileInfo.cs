using System.IO;

namespace CC98.Services;

/// <summary>
///     表示要上传的文件信息。
/// </summary>
public class UploadFileInfo
{
	/// <summary>
	///     上传的文件的文件名称。
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	///     上传的文件的数据流。
	/// </summary>
	public Stream Stream { get; set; }
}