namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
/// 表示单个命中的线索信息。
/// </summary>
public interface IHitInfo
{
	/// <summary>
	/// 命中的线索的内容。
	/// </summary>
	string Value { get; set; }
}