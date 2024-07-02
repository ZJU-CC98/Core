namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 为只有 <see cref="Details"/> 字段的服务响应提供封装。
/// </summary>
/// <typeparam name="T">服务响应包含的详细数据的类型。</typeparam>
public class SimpleResponseItemCollection<T> : ResponseItemBase
{
	/// <summary>
	/// 获取或设置响应的详细数据内容的集合。
	/// </summary>
	public T[]? Details { get; set; }
}