using JetBrains.Annotations;

namespace CC98.Identity
{
    /// <summary>
    ///     包含 CC98-API 接口定义的标准领域类型。该类型为静态类型。
    /// </summary>
    [PublicAPI]
    public static class ApiScopes
    {
        /// <summary>
        /// 表示所有 CC98 API 的相关权限。该字段为常量。
        /// </summary>
	    public const string All = "cc98-api";

        /// <summary>
        /// 表示向服务器上传文件的权限。该字段为常量。
        /// </summary>
        public const string FileUpload = "file-upload";
    }
}