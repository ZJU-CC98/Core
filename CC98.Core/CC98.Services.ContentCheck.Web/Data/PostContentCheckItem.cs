using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
///     表示对文本的审核记录。
/// </summary>
[Index(nameof(PostId), IsUnique = true)]
internal class PostContentCheckItem : ContentCheckItem
{
	/// <summary>
	///     获取或设置该项目关联到的发言的标识。
	/// </summary>
	public int PostId { get; set; }
}