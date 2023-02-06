namespace CC98;

/// <summary>
///     定义 <see cref="AppSettingService{T}.Current" /> 的加载模式。
/// </summary>
public enum AppSettingLoadMode
{
	/// <summary>
	///     当服务对象初始化时自动加载设置。
	/// </summary>
	Auto,

	/// <summary>
	///     当第一次访问 <see cref="AppSettingService{T}.Current" /> 时加载设置。
	/// </summary>
	AutoDelayed,

	/// <summary>
	///     不使用缓存，每次访问设置都重新从数据加载数据。这可能严重影响性能。
	/// </summary>
	Always,

	/// <summary>
	///     除非使用 <see cref="AppSettingService{T}.LoadAsync" /> 方法加载数据，否则绝不自动加载设置。
	/// </summary>
	Manual
}