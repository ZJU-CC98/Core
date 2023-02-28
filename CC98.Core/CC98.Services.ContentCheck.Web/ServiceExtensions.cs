using CC98.Services.ContentCheck.EaseDun;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 为内容审查服务配置提供扩展方法。该类型为静态类型。
/// </summary>
public static class ServiceExtensions
{
	/// <summary>
	/// 配置数据在数据库中的名称。
	/// </summary>
	private const string AppName = "ContentCheck";

	/// <summary>
	/// 为应用添加内容审查服务。
	/// </summary>
	/// <param name="services">服务容器。</param>
	/// <param name="connectionString">内容审查服务的连接字符串。</param>
	public static IServiceCollection AddContentCheck(this IServiceCollection services, string connectionString)
	{
		services.AddAppSetting<ContentCheckSystemSetting>()
			.AddAccess(options =>
			{
				options.AppName = AppName;
				options.DataFormat = AppSettingFormats.Json;
			})
			.AddSqlServer(connectionString);

		return services;
	}

	/// <summary>
	///     添加网易易盾服务。
	/// </summary>
	/// <param name="services">服务容器。</param>
	/// <param name="configuration">服务相关的配置设置。</param>
	/// <returns><paramref name="services" /> 对象。</returns>
	public static IServiceCollection AddEaseDun(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<EaseDunWebService>();
		services.Configure<EaseDunOptions>(configuration);

		return services;
	}
}