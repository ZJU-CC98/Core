using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CC98.Services.Web
{
	/// <summary>
	/// 提供 <see cref="IServiceCollection"/> 的扩展方法。该类型为静态类型。
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// 为项目添加 <see cref="FileUploadService"/> 服务实现。
		/// </summary>
		/// <param name="services">服务容器对象。</param>
		/// <param name="configOptions">用于配置文件传输服务配置的配置方法。</param>
		/// <returns><paramref name="services"/> 对象。</returns>
		public static IServiceCollection AddCC98FileUploadWebService(this IServiceCollection services, Action<FileUploadServiceConfig> configOptions = null)
		{
			services.TryAddSingleton<FileUploadWebService>();

			if (configOptions != null)
			{
				services.Configure(configOptions);
			}

			return services;
		}
	}
}
