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
	public required string Name { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序的描述。
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序的主页地址。
	/// </summary>
	[Url]
	public string? HomePageUri { get; set; }

	/// <summary>
	///     获取或设置该服务提供程序支持的功能类型。
	/// </summary>
	public required ContentCheckServiceType[] ServiceTypes { get; set; }
}