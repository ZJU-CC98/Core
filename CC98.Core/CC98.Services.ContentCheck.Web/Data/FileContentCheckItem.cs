using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示对文件的检查信息。
/// </summary>
[Index(nameof(FileId), IsUnique = true)]
public class FileContentCheckItem : ContentCheckItem
{
	/// <summary>
	/// 获取或设置该项目关联到的文件的标识。
	/// </summary>
	public int FileId { get; set; }
}