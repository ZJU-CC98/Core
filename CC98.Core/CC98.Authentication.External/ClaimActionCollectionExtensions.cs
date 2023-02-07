#if NETSTANDARD2_0
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace CC98.Authentication
{
    /// <summary>
    ///     �ṩ���� <see cref="ClaimActionCollection" /> ���͵ĸ���������������Ϊ��̬���͡�
    /// </summary>
    public static class ClaimActionCollectionExtensions
    {
        /// <summary>
        ///     Ϊ <see cref="ClaimActionCollection" /> ���һ�� <see cref="ArrayJsonKeyClaimAction" /> ������������ֵ���ͱ�����Ϊ
        ///     <see cref="ClaimValueTypes.String" />��
        /// </summary>
        /// <param name="claimActions">Ҫ��ӷ����� <see cref="ClaimActionCollection" /> ����</param>
        /// <param name="claimType">���������͡�</param>
        /// <param name="jsonKey">Ҫӳ��� JSON ��������</param>
        public static void MapArrayJsonKey(this ClaimActionCollection claimActions, string claimType, string jsonKey)
        {
            claimActions.MapArrayJsonKey(claimType, jsonKey, ClaimValueTypes.String);
        }

        /// <summary>
        ///     Ϊ <see cref="ClaimActionCollection" /> ���һ�� <see cref="ArrayJsonKeyClaimAction" /> ����
        /// </summary>
        /// <param name="claimActions">Ҫ��ӷ����� <see cref="ClaimActionCollection" /> ����</param>
        /// <param name="claimType">���������͡�</param>
        /// <param name="jsonKey">Ҫӳ��� JSON ��������</param>
        /// <param name="valueType">������ֵ�����͡�</param>
        public static void MapArrayJsonKey(this ClaimActionCollection claimActions, string claimType, string jsonKey,
            string valueType)
        {
            claimActions.Add(new ArrayJsonKeyClaimAction(claimType, valueType, jsonKey));
        }
    }
}

#endif