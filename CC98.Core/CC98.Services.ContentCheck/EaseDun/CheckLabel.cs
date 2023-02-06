namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     系统定义的检查标签设置。
/// </summary>
public class CheckLabel
{
	/// <summary>
	///     标签对应的标识。
	/// </summary>
	public required int Id { get; set; }

	/// <summary>
	///     标签的名称。
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     可选的描述。
	/// </summary>
	public string? Description { get; set; }
}