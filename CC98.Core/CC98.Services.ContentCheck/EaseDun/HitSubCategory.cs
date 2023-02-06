namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     表示一个命中的子标签。
/// </summary>
public class HitSubCategory
{
	/// <summary>
	///     子标签的标识。
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	///     子标签的名称。
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     子标签的描述。
	/// </summary>
	public string? Description { get; set; }
}