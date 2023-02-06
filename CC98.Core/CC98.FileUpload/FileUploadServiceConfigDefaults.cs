namespace CC98.Services;

/// <summary>
/// 提供 <see cref="FileUploadServiceConfig"/> 相关设置的默认值。该类型为静态类型。
/// </summary>
public static class FileUploadServiceConfigDefaults
{
	/// <summary>
	/// <see cref="FileUploadServiceConfig.Authority"/> 的默认值。该字段为常量。
	/// </summary>
	public const string Authoriy = "https://openid.cc98.org";
	/// <summary>
	/// <see cref="FileUploadServiceConfig.ApiUri"/> 的默认值。该字段为常量。
	/// </summary>
	public const string ApiUri = "https://api.cc98.org/file/service";
	/// <summary>
	/// <see cref="FileUploadServiceConfig.FileFormKey"/> 的默认值。该字段为常量。
	/// </summary>
	public const string FileFormKey = "files";
	/// <summary>
	/// <see cref="FileUploadServiceConfig.SubPathKey"/> 的默认值。该字段为常量。
	/// </summary>
	public const string SubPathKey = "subPath";
	/// <summary>
	/// <see cref="FileUploadServiceConfig.CompressFormKey"/> 的默认值。该字段为常量。
	/// </summary>
	public const string CompressFormKey = "compressImage";
}