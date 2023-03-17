using System;

using CC98.Services.ContentCheck.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CC98.Services.ContentCheck;

/// <summary>
///     为内容审查服务配置提供扩展方法。该类型为静态类型。
/// </summary>
public static class ServiceExtensions
{
	/// <summary>
	///     配置数据在数据库中的名称。
	/// </summary>
	private const string AppName = "ContentCheck";

	/// <summary>
	///     为应用添加内容审查服务。
	/// </summary>
	/// <param name="services">服务容器。</param>
	/// <param name="contentCheckDbContextOptionsBuilder">内容审查服务数据库配置操作。</param>
	/// <param name="appSettingDbContextOptionsBuilder">应用程序设置数据库配置操作。</param>
	/// <returns>一个 <see cref="ContentCheckServiceBuilder" /> 对象，可用于后续进一步进行额外配置。</returns>
	public static ContentCheckServiceBuilder AddContentCheck(this IServiceCollection services,
		Action<DbContextOptionsBuilder>? contentCheckDbContextOptionsBuilder, Action<DbContextOptionsBuilder>? appSettingDbContextOptionsBuilder)
	{
		services.AddDbContext<ContentCheckDbContext>(contentCheckDbContextOptionsBuilder);

		services.AddAppSetting<ContentCheckSystemSetting>()
			.AddAccess(options =>
			{
				options.AppName = AppName;
				options.DataFormat = AppSettingFormats.Json;
			})
			.AddDbContext(appSettingDbContextOptionsBuilder);

		services.TryAddSingleton<ContentCheckService>();

		return new(services);
	}
}