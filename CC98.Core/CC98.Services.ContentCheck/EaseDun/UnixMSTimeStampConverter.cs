using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 提供从 <see cref="DateTimeOffset"/> 到 <see cref="long"/> 类型的转换。
/// </summary>
public class UnixMSTimeStampConverter : JsonConverter<DateTimeOffset>
{
	/// <inheritdoc />
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetInt64();
		return DateTimeOffset.FromUnixTimeMilliseconds(value);
	}

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		var realValue = value.ToUnixTimeMilliseconds();
		writer.WriteNumberValue(realValue);
	}
}