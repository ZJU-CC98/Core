using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     为所有响应提供公共抽象基础类型。
/// </summary>
/// <typeparam name="T"></typeparam>
public class CommonResponseBody<T>
{
	/// <summary>
	///     响应代码。正常为 200。
	/// </summary>
	public required int Code { get; set; }

	/// <summary>
	///     响应消息。正确时返回 ok，错误时返回错误消息。
	/// </summary>
	[JsonPropertyName("msg")]
	public required string Message { get; set; }

	/// <summary>
	///     响应的结果数据。
	/// </summary>
	public T? Result { get; set; }
}