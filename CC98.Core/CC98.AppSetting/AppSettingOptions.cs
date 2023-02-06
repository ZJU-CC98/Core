namespace CC98;

/// <summary>
///     表示应用程序的选项相关功能的设置。
/// </summary>
/// <typeparam name="T">保存应用程序设置的数据对象类型。</typeparam>
public class AppSettingOptions<T>
	where T : class
{
	/// <summary>
	///     获取或设置当应用程序设置对象不存在时使用的默认设置值。
	/// </summary>
	public T DefaultSetting { get; set; } = null!;

	/// <summary>
	///     获取或设置应用设置的保存模式。
	/// </summary>
	public AppSettingSaveMode SaveMode { get; set; } = AppSettingSaveMode.Auto;

	/// <summary>
	///     获取或设置应用设置的加载模式。
	/// </summary>
	public AppSettingLoadMode LoadMode { get; set; } = AppSettingLoadMode.Auto;
}