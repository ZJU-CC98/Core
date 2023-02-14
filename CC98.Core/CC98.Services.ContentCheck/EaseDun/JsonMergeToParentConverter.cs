using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 提供将强类型对象中的子属性合并到父类进行 JSON 序列化的实现。
/// </summary>
public class JsonMergeToParentConverter : JsonConverter<object>
{
	/// <inheritdoc />
	public override bool HandleNull => true;

	/// <inheritdoc />
	public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotSupportedException("不支持在反序列化中使用该特性。");
	}

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
	{
		// 忽略 null 值。
		if (value == null)
		{
			return;
		}

		var newElement = JsonSerializer.SerializeToElement(value, value.GetType(), options);

		foreach (var node in newElement.EnumerateObject())
		{
			node.WriteTo(writer);
		}
	}
}