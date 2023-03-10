using System;
using System.Collections.Generic;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 内容审查服务的内部配置选项信息。
/// </summary>
public class ContentCheckOptions
{
	/// <summary>
	/// 获取目前已注册的所有服务提供程序。
	/// </summary>
	public IDictionary<string, Type> ServiceProviders { get; } = new Dictionary<string, Type>();
}