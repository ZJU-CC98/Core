namespace CC98.Services.ContentCheck.EaseDun.Native.Texts;

/// <summary>
/// 为所有响应对象提供基础类型。
/// </summary>
public class ResponseItemBase
{
	/// <summary>
	///     检测任务的标识。该字段为网易易盾内部使用，对用户无意义。
	/// </summary>
	public required string TaskId { get; set; }

	/// <summary>
	///     数据标识，用于指示本结果对应的检测请求。该字段等同于请求时的 <see cref="TextItem.DataId" /> 字段。
	/// </summary>
	public required string DataId { get; set; }
}