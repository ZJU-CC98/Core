#if NETSTANDARD1_3 || NET451
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CC98.Authentication
{
	/// <summary>
	///     表示 CC98 身份验证中间件。
	/// </summary>
	public class CC98AuthenticationMiddleware : OAuthMiddleware<CC98AuthenticationOptions>
	{
		/// <summary>
		///     初始化一个对象的新实例。
		/// </summary>
		/// <param name="next">下一个中间件对象。</param>
		/// <param name="dataProtectionProvider">数据保护提供程序。</param>
		/// <param name="loggerFactory">日志工厂。</param>
		/// <param name="encoder">URL 编码器。</param>
		/// <param name="sharedOptions">共享身份验证选项。</param>
		/// <param name="options">其它选项。</param>
		public CC98AuthenticationMiddleware(RequestDelegate next, IDataProtectionProvider dataProtectionProvider,
			ILoggerFactory loggerFactory, UrlEncoder encoder, IOptions<SharedAuthenticationOptions> sharedOptions,
			IOptions<CC98AuthenticationOptions> options)
			: base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
		{
		}

		/// <summary>
		///     创建身份验证中间件对应的验证处理程序。
		/// </summary>
		/// <returns>该身份验证中间件对应的处理程序。</returns>
		protected override AuthenticationHandler<CC98AuthenticationOptions> CreateHandler()
		{
			return new CC98AuthenticationHandler(Backchannel);
		}
	}
}

#endif