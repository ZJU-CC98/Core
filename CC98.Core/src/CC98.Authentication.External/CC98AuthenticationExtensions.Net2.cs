#if NETSTANDARD2_0

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CC98.Authentication
{
    /// <summary>
    ///     为 CC98 身份验证模块提供辅助方法。该类型为静态类型。
    /// </summary>
    public static class CC98AuthenticationExtensions
    {
        /// <summary>
        /// 为应用程序添加 CC98 验证服务。使用所有默认设置。
        /// </summary>
        /// <param name="builder">应用程序验证服务对象。</param>
        /// <returns><paramref name="builder"/> 参数。</returns>
        public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder)

            => builder.AddCC98(CC98AuthenticationDefaults.AuthenticationScheme, _ => { });

        /// <summary>
        /// 为应用程序添加 CC98 验证服务。使用所有默认设置。
        /// </summary>
        /// <param name="builder">应用程序验证服务对象。</param>
        /// <param name="configureOptions">配置 CC98 验证选项的额外配置方法。</param>
        /// <returns><paramref name="builder"/> 参数。</returns>
        public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, Action<CC98AuthenticationOptions> configureOptions)

            => builder.AddCC98(CC98AuthenticationDefaults.AuthenticationScheme, configureOptions);


        /// <summary>
        /// 为应用程序添加 CC98 验证服务。使用所有默认设置。
        /// </summary>
        /// <param name="builder">应用程序验证服务对象。</param>
        /// <param name="authenticationScheme">在应用程序中需要使用的验证架构名称。</param>
        /// <param name="configureOptions">配置 CC98 验证选项的额外配置方法。</param>
        /// <returns><paramref name="builder"/> 参数。</returns>
        public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, string authenticationScheme, Action<CC98AuthenticationOptions> configureOptions)

            => builder.AddCC98(authenticationScheme, CC98AuthenticationDefaults.DisplayName, configureOptions);

        /// <summary>
        /// 为应用程序添加 CC98 验证服务。使用所有默认设置。
        /// </summary>
        /// <param name="builder">应用程序验证服务对象。</param>
        /// <param name="authenticationScheme">在应用程序中需要使用的验证架构名称。</param>
        /// <param name="displayName">该验证方法的显示名称。</param>
        /// <param name="configureOptions">配置 CC98 验证选项的额外配置方法。</param>
        /// <returns><paramref name="builder"/> 参数。</returns>
        public static AuthenticationBuilder AddCC98(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CC98AuthenticationOptions> configureOptions)
            => builder.AddOAuth<CC98AuthenticationOptions, CC98AuthenticationHandler>(authenticationScheme, displayName, configureOptions);
    }
}

#endif