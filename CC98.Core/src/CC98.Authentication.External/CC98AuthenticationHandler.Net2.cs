#if NETSTANDARD2_0

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CC98.Authentication
{


    /// <summary>
    ///     表示 CC98 身份验证处理程序。
    /// </summary>
    internal class CC98AuthenticationHandler : OAuthHandler<CC98AuthenticationOptions>
    {
        /// <summary>
        /// 初始化一个 CC98 身份验证处理程序的新实例。
        /// </summary>
        /// <param name="options">选项监视服务。</param>
        /// <param name="logger">日志工厂服务。</param>
        /// <param name="encoder">URL 编码服务。</param>
        /// <param name="clock">系统时钟服务。</param>

        public CC98AuthenticationHandler(IOptionsMonitor<CC98AuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity,
            AuthenticationProperties properties, OAuthTokenResponse tokens)
        {

            // 消息内容
            var message = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);

            // Authorization 标头
            message.Headers.Authorization = new AuthenticationHeaderValue(tokens.TokenType, tokens.AccessToken);

            // 发送 GET 请求
            var response = await Backchannel.SendAsync(message);

            // 响应状态查询
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("CC98 Identity 框架无法加载用户个人信息，HTTPClient 返回代码：{0}", response.StatusCode);
                return null;
            }

            // 获得内容
            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            // 执行声明处理
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions();

            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);

        }

    }
}

#endif