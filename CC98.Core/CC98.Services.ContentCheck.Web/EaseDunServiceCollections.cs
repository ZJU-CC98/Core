using CC98.Services.ContentCheck.EaseDun;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CC98.Services.ContentCheck;

/// <summary>
///     为网易易盾服务支持提供扩展方法。该类型为静态类型。
/// </summary>
public static class EaseDunServiceCollections
{
	/// <summary>
	///     添加网易易盾服务核心功能。
	/// </summary>
	/// <param name="services">服务容器。</param>
	/// <param name="configuration">服务相关的配置设置。</param>
	/// <returns><paramref name="services" /> 对象。</returns>
	/// <remarks>一般不直接使用此方法，请考虑使用 <see cref="AddEaseDun" /> 方法直接添加网易易盾内容审核服务及其核心功能。</remarks>
	public static IServiceCollection AddEaseDunCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.TryAddScoped<EaseDunWebService>();
		services.Configure<EaseDunOptions>(configuration);

		return services;
	}

	/// <summary>
	///     添加网易易盾提供的内容审核服务。
	/// </summary>
	/// <param name="builder">用于配置内容审核服务的配置对象。</param>
	/// <param name="configuration">网易易盾服务所必须的相关配置数据。</param>
	/// <returns><paramref name="builder" /> 对象。</returns>
	public static ContentCheckServiceBuilder AddEaseDun(this ContentCheckServiceBuilder builder,
		IConfiguration configuration)
	{
		builder.ServiceCollection.AddEaseDunCore(configuration);
		builder.AddServiceProvider<EaseDunWebService>();

		return builder;
	}
}