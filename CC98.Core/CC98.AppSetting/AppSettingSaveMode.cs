namespace CC98;

/// <summary>
///     定义 <see cref="AppSettingService{T}.Current" /> 的保存模式。
/// </summary>
public enum AppSettingSaveMode
{
	/// <summary>
	///     当修改 <see cref="AppSettingService{T}.Current" /> 属性时自动保存设置。
	/// </summary>
	Auto,

	/// <summary>
	///     用户必须使用 <see cref="AppSettingService{T}.SaveAsync" /> 方法手动保存设置。
	/// </summary>
	Manual
}