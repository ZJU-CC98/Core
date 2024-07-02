using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 为服务配置提供扩展方法。该类型为静态类型。
/// </summary>
[PublicAPI]
public static class ServiceExtensions
{
	/// <summary>
	/// 添加 <see cref="ZuaaTokenService"/>
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddZuaaTokenService(this IServiceCollection services)
	{
		services.TryAddSingleton<ZuaaTokenService>();
		return services;
	}
}