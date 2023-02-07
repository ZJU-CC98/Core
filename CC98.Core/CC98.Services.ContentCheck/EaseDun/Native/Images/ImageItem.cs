using System.ComponentModel.DataAnnotations;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
///     表示图片检测中的单个图片项。
/// </summary>
public class ImageItem
{
	/// <summary>
	///     图片的名称或标识。检测服务本身不使用该字段，可用于在回调中区分检测对象。
	/// </summary>
	[StringLength(1024)]
	public required string Name { get; set; }

	/// <summary>
	///     图片数据的类型。
	/// </summary>
	public required ImageDataType Type { get; set; }

	/// <summary>
	///     图片数据。根据 <see cref="Type" /> 属性的设置，可能为图片地址或图片内容的 Base64 字符串。
	/// </summary>
	public required string Data { get; set; }

	/// <summary>
	///     使用异步检测模式时，检测结果产生后需要使用的回调地址。
	/// </summary>
	[DataType(DataType.Url)]
	[Url]
	[StringLength(256)]
	public string? CallbackUrl { get; set; }

	/// <summary>
	///     本次数据的唯一标识。由于 <see cref="Name" /> 可能在多次编辑中相同，可用本属性标识同一内容的多次编辑版本。
	/// </summary>
	[StringLength(128)]
	public string? DataId { get; set; }
}