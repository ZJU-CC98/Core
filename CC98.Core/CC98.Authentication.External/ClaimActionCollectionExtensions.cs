#if NETSTANDARD2_0
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace CC98.Authentication
{
    /// <summary>
    ///     提供对于 <see cref="ClaimActionCollection" /> 类型的辅助方法。该类型为静态类型。
    /// </summary>
    public static class ClaimActionCollectionExtensions
    {
        /// <summary>
        ///     为 <see cref="ClaimActionCollection" /> 添加一个 <see cref="ArrayJsonKeyClaimAction" /> 对象。新声明的值类型被定义为
        ///     <see cref="ClaimValueTypes.String" />。
        /// </summary>
        /// <param name="claimActions">要添加方法的 <see cref="ClaimActionCollection" /> 对象。</param>
        /// <param name="claimType">声明的类型。</param>
        /// <param name="jsonKey">要映射的 JSON 属性名。</param>
        public static void MapArrayJsonKey(this ClaimActionCollection claimActions, string claimType, string jsonKey)
        {
            claimActions.MapArrayJsonKey(claimType, jsonKey, ClaimValueTypes.String);
        }

        /// <summary>
        ///     为 <see cref="ClaimActionCollection" /> 添加一个 <see cref="ArrayJsonKeyClaimAction" /> 对象。
        /// </summary>
        /// <param name="claimActions">要添加方法的 <see cref="ClaimActionCollection" /> 对象。</param>
        /// <param name="claimType">声明的类型。</param>
        /// <param name="jsonKey">要映射的 JSON 属性名。</param>
        /// <param name="valueType">声明的值的类型。</param>
        public static void MapArrayJsonKey(this ClaimActionCollection claimActions, string claimType, string jsonKey,
            string valueType)
        {
            claimActions.Add(new ArrayJsonKeyClaimAction(claimType, valueType, jsonKey));
        }
    }
}

#endif