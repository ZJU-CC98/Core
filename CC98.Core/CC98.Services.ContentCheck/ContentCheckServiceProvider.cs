using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义内容审查服务的提供程序。
/// </summary>
public class ContentCheckServiceProvider
{
	/// <summary>
	///     获取或设置该服务提供程序的名称。
	/// </summary>
	[Display(Name = "名称")]
	public required string Name { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序的描述。
	/// </summary>
	[Display(Name = "描述")]
	public string? Description { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序的主页地址。
	/// </summary>
	[Url]
	[Display(Name = "主页")]
	public string? HomePageUri { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序支持的功能类型。
	/// </summary>
	[Display(Name = "支持类型")]
	public required ContentCheckServiceType[] ServiceTypes { get; set; }
}