#if NETSTANDARD1_4 || NET451

namespace CC98.Authentication.OpenIdConnect
{
	/// <summary>
	///     为 <see cref="IApplicationBuilder" /> 提供扩展方法。该类型为静态类型。
	/// </summary>
	public static class AppBuilderExtensions
	{
		/// <summary>
		///     配置应用程序使用 CC98 身份验证服务。
		/// </summary>
		/// <param name="app">应用程序对象。</param>
		/// <param name="clientId">CC98 身份验证客户端标识。</param>
		/// <param name="clientSecret">CC98 身份验证客户端密钥。</param>
		/// <returns></returns>
		public static IApplicationBuilder UseCC98(this IApplicationBuilder app, string clientId, string clientSecret)
		{
			var options = new OpenIdConnectOptions
			{
				AuthenticationScheme = CC98Defaults.AuthenticationScheme,
				Authority = CC98Defaults.Authority,
				ClientId = clientId,
				ClientSecret = clientSecret
			};

			return app.UseOpenIdConnectAuthentication(options);
		}
	}
}

#endif