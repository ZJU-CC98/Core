using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;

#if NETSTANDARD2_0
namespace CC98.Authentication.OpenIdConnect
{
	/// <summary>
	/// 为 <see cref="IServiceCollection"/> 添加服务提供扩展方法。该类型为静态类型。
	/// </summary>
	public static class ServiceCollectionExtensions
	{

		public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder)

			=> builder.AddCC98(CC98Defaults.AuthenticationScheme, _ => { });



		public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)

			=> builder.AddCC98(CC98Defaults.AuthenticationScheme, configureOptions);



		public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)

			=> builder.AddCC98(authenticationScheme, CC98Defaults.DisplayName, configureOptions);


		/// <summary>
		/// 用指定的信息为当前应用程序添加
		/// </summary>
		/// <param name="builder">应用程序对象。</param>
		/// <param name="authenticationScheme">在身份验证过程中使用的架构名称。</param>
		/// <param name="displayName">身份验证方法的显示名称。</param>
		/// <param name="configureOptions">需要对 <see cref="OpenIdConnectOptions"/> 进行的额外配置过程。</param>
		/// <returns><paramref name="builder"/> 对象。</returns>
		public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, string authenticationScheme,
			string displayName, Action<OpenIdConnectOptions> configureOptions)
		{
			// 包装实际需要使用的配置方法。
			void RealConfig(OpenIdConnectOptions options)
			{
				ConfigureOpenIdConnectOptions(options);
				configureOptions?.Invoke(options);
			}

			return builder.AddOpenIdConnect(authenticationScheme, displayName, RealConfig);
		}

		/// <summary>
		/// 针对 CC98 身份验证服务提供对于 <see cref="OpenIdConnectOptions"/> 的默认配置方法。
		/// </summary>
		/// <param name="options">要配置的 <see cref="OpenIdConnectOptions"/> 对象。</param>
		public static void ConfigureOpenIdConnectOptions(OpenIdConnectOptions options)
		{
			options.Authority = CC98Defaults.Authority;
		}

	}
}

#endif