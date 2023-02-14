using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using CC98.Services.ContentCheck.EaseDun.Native.Images;
using CC98.Services.ContentCheck.EaseDun.Native.Texts;

using JetBrains.Annotations;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     为所有易盾检测请求提供公共基础参数。
/// </summary>
[PublicAPI]
[JsonDerivedType(typeof(TextSingleDetectRequestBody))]
[JsonDerivedType(typeof(TextBatchDetectRequestBody))]
[JsonDerivedType(typeof(ImageDetectRequestBody))]
public class CommonRequestBody
{
	/// <summary>
	///     请求密钥。
	/// </summary>
	[StringLength(32)]
	public required string SecretId { get; set; }

	/// <summary>
	///     业务 ID。
	/// </summary>
	[StringLength(32)]
	public required string BusinessId { get; set; }

	/// <summary>
	///     请求时间。Unix毫秒时间戳格式。
	/// </summary>
	[JsonConverter(typeof(UnixMSTimeStampConverter))]
	public required DateTimeOffset Timestamp { get; set; }

	/// <summary>
	///     随机一次性请求值。用于防止重放攻击。
	/// </summary>
	public required long Nonce { get; set; }

	/// <summary>
	///     签名算法。使用 SM3 表示国密算法。不提供则使用默认 MD5.
	/// </summary>
	public string? SignatureMethod { get; set; }

	/// <summary>
	///     请求签名。该信息将在 <see cref="GenerateSignature" /> 方法中自动生成。
	/// </summary>
	[StringLength(32)]
	public string? Signature { get; private set; }

	/// <summary>
	///     为该对象生成签名并产生请求主体数据。
	/// </summary>
	public IDictionary<string, string> GenerateSignature(string secretKey)
	{
		var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};

		var content = JsonSerializer.SerializeToElement(this, options);

		// 将请求对象转存为字典格式
		var bodyDictionary =
			content.EnumerateObject().ToDictionary(property => property.Name, property => property.Value.ToString());

		// 使用密钥生成签名
		Signature = EaseDunUtility.GenerateSignature(secretKey, bodyDictionary);

		// 添加签名数据
		bodyDictionary.Add("signature", Signature);
		return bodyDictionary;
	}
}