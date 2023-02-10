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
	///     为设置服务添加数据库连接。
	/// </summary>
	/// <param name="connectionString">数据库连接字符串。</param>
	/// <param name="configOptions">额外的数据库对象。</param>
	/// <returns>当前对象。</returns>
	public AppSettingServiceBuilder AddSqlServer(string connectionString,
		Action<SqlServerDbContextOptionsBuilder>? configOptions = null)
	{
		Services.AddDbContext<CC98V2DbContext>(
			options => { options.UseSqlServer(connectionString, configOptions); });
		return this;
	}
}