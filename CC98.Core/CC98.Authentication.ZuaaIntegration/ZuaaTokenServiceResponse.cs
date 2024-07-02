using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 定义浙大校友令牌身份验证服务的响应的通用格式。
/// </summary>
/// <typeparam name="T">响应的具体数据的类型。</typeparam>
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature | ImplicitUseKindFlags.Assign,
	ImplicitUseTargetFlags.Members)]
public class ZuaaTokenServiceResponse<T>
{
	/// <summary>
	/// 响应的代码。200 表示正确响应，否则表示出现异常。
	/// </summary>
	[JsonPropertyName("code")]
	public int Code { get; set; }

	/// <summary>
	/// 响应的异常相关消息。
	/// </summary>
	[JsonPropertyName("msg")]
	public string? Message { get; set; }

	/// <summary>
	/// 响应为集合时，包含的数据总数。
	/// </summary>
	[JsonPropertyName("total")]
	public int Total { get; set; }

	/// <summary>
	/// 响应的详细数据内容。
	/// </summary>
	[JsonPropertyName("data")]
	public required T? Data { get; set; }

	/// <summary>
	/// 获取一个值，指示当前相应是否表示成功。
	/// </summary>
	[JsonIgnore]
	[MemberNotNullWhen(true, nameof(Data))]
	public bool IsSucceeded => Code == 200;

	/// <summary>
	/// 确保该响应表示成功，否则引发异常。
	/// </summary>
	/// <exception cref="ZuaaTokenServiceException">该响应不是成功响应。</exception>
	[MemberNotNull(nameof(Data))]
	public void EnsureSuccess()
	{
		if (!IsSucceeded)
		{
			throw new ZuaaTokenServiceException($"浙大校友服务器响应返回异常。响应代码: {Code}，错误消息：{Message}。");
		}
	}
}