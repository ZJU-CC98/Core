#if NETSTANDARD2_0

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Newtonsoft.Json.Linq;

namespace CC98.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// 提供将类型为数组的 JSON 属性映射到多个声明的方法。
    /// </summary>
    public class ArrayJsonKeyClaimAction : ClaimAction
    {
        /// <summary>
        /// 要映射的 JSON 属性名称。
        /// </summary>
        public string JsonKey { get; }

        /// <inheritdoc />
        /// <summary>
        /// 初始化一个 <see cref="T:CC98.Authentication.ArrayJsonKeyClaimAction" /> 对象的新实例。
        /// </summary>
        /// <param name="claimType">声明的类型。</param>
        /// <param name="valueType">声明的值的类型。</param>
        /// <param name="jsonKey">要映射的 JSON 属性名。</param>
        public ArrayJsonKeyClaimAction(string claimType, string valueType, string jsonKey)
            : base(claimType, valueType)
        {
            JsonKey = jsonKey;
        }

        public override void Run(JObject userData, ClaimsIdentity identity, string issuer)
        {
            var items = userData[JsonKey];

            if (items is JArray array)
            {
                foreach (var item in array)
                {
                    var claim = new Claim(ClaimType, (string)item, ValueType, issuer);
                    identity.AddClaim(claim);
                }
            }
        }
    }
}

#endif