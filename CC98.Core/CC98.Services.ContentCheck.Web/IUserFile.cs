using System;

namespace CC98.Services.ContentCheck;

/// <summary>
///     表示用户上传的文件。
/// </summary>
public interface IUserFile
{
	/// <summary>
	///     文件的标识。
	/// </summary>
	int Id { get; }

	/// <summary>
	///     上传的文件在系统中的完整存储路径。
	/// </summary>
	string FilePath { get; }

	/// <summary>
	///     上传的用户标识。
	/// </summary>
	int UploadUserId { get; }

	/// <summary>
	///     上传的时间。
	/// </summary>
	DateTimeOffset UploadTime { get; }
}