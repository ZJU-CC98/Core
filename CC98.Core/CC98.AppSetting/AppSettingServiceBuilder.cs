using System;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CC98;

/// <summary>
///     为 <see cref="AppSettingService{T}" /> 提供访问设置的相关设置。
/// </summary>
public class AppSettingServiceBuilder
{
	/// <summary>
	///     初始化一个对象的新实例。
	/// </summary>
	/// <param name="services">服务设置容器。</param>
	internal AppSettingServiceBuilder(IServiceCollection services)
	{
		Services = services;
	}

	/// <summary>
	/// 服务容器对象。
	/// </summary>
	private IServiceCollection Services { get; }

	/// <summary>
	///     为设置服务添加基础访问功能。
	/// </summary>
	/// <param name="setupAction">访问服务的相关设置。</param>
	/// <returns>当前对象。</returns>
	[PublicAPI]
	public AppSettingServiceBuilder AddAccess(Action<AppSettingAccessOptions>? setupAction = null)
	{
		Services.AddAppSettingAccess(setupAction);
		return this;
	}

	/// <summary>
	///     为设置服务配置添加数据库支持。。
	/// </summary>
	/// <param name="configOptions">数据库配置操作。</param>
	/// <returns>当前对象。</returns>
	public AppSettingServiceBuilder AddDbContext(Action<DbContextOptionsBuilder>? configOptions)
	{
		Services.AddDbContext<CC98V2DbContext>(configOptions);
		return this;
	}
}