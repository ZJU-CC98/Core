using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CC98.Management.Services.EaseDun;

/// <summary>
///     提供服务扩展方法。该类型为静态类型。
/// </summary>
public static class ServiceExtensions
{
	/// <summary>
	///     添加网易易盾服务。
	/// </summary>
	/// <param name="services">服务容器。</param>
	/// <param name="configuration">服务相关的配置设置。</param>
	/// <returns><paramref name="services" /> 对象。</returns>
	public static IServiceCollection AddEaseDun(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<EaseDunWebService>();
		services.Configure<EaseDunOptions>(configuration);

		return services;
	}
}