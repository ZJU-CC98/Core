using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     为网易易盾服务提供辅助方法。该类型为静态类型。
/// </summary>
public static class EaseDunUtility
{
	/// <summary>
	///     使用给定的用户密钥，对传输参数生成签名。
	/// </summary>
	/// <param name="secretKey">用户密钥。</param>
	/// <param name="parameters">包含所有传输参数名称和值字典。</param>
	/// <returns></returns>
	public static string GenerateSignature(string secretKey, IDictionary<string, string> parameters)
	{
		var items = parameters.OrderBy(o => o.Key, StringComparer.Ordinal);

		var builder = new StringBuilder();

		foreach (var (key, value) in items) builder.Append(key).Append(value);

		builder.Append(secretKey);

		var result = MD5.HashData(Encoding.UTF8.GetBytes(builder.ToString()));

		builder.Clear();
		foreach (var b in result) builder.Append($"{b:x2}");
		return builder.ToString();
	}
}