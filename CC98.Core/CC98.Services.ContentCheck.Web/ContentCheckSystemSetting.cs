namespace CC98.Services.ContentCheck;

/// <summary>
/// 表示内容审查服务的系统级设置。
/// </summary>
public class ContentCheckSystemSetting : IAppSettingWithDefaultValue<ContentCheckSystemSetting>
{
	/// <summary>
	/// 获取或设置全局默认的内容审查设置。
	/// </summary>
	public required ContentCheckSetting Global { get; set; }

	/// <summary>
	/// 获取系统设置的默认值。
	/// </summary>
	static ContentCheckSystemSetting IAppSettingWithDefaultValue<ContentCheckSystemSetting>.Default { get; } = new()
	{
		Global = new()
		{
			RecordLevel = ContentCheckRecordLevel.All,
			ServiceProvider = "EaseDun",
			CheckTypes = new()
		}
	};
}