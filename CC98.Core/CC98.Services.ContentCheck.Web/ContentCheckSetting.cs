using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义论坛/版面的内容审查设置。
/// </summary>
public class ContentCheckSetting : IJsonOnDeserialized
{
	/// <summary>
	///     需要使用的审查服务提供程序。
	/// </summary>
	public required string ServiceProvider { get; set; }

	/// <summary>
	///     需要默认启用的审查内容和对应的分类设置。
	/// </summary>
	public Dictionary<ContentCheckServiceType, ContentCheckTypeSetting> CheckTypes { get; set; }
		= new()
		{
			[ContentCheckServiceType.Text] = new(),
			[ContentCheckServiceType.Image] = new(),
			[ContentCheckServiceType.Audio] = new(),
			[ContentCheckServiceType.Video] = new()
		};

	/// <summary>
	///     审查的默认记录级别。
	/// </summary>
	public required ContentCheckRecordLevel RecordLevel { get; set; }

	/// <inheritdoc />
	public void OnDeserialized()
	{
		CheckTypes.TryAdd(ContentCheckServiceType.Text, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Image, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Audio, new());
		CheckTypes.TryAdd(ContentCheckServiceType.Video, new());
	}
}