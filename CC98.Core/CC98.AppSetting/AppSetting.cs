using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98;

/// <summary>
///     定义一个应用程序设置。
/// </summary>
[Table("AppSettingSet")]
public class AppSetting
{
	/// <summary>
	///     获取或设置设置的标识。
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	///     获取或设置应用的名称。
	/// </summary>
	[StringLength(50)]
	[Required]
	public string AppName { get; set; }

	/// <summary>
	///     获取或设置数据的格式。
	/// </summary>
	[StringLength(50)]
	public string Format { get; set; }

	/// <summary>
	///     获取或设置应用的数据。
	/// </summary>
	public byte[] Data { get; set; }
}