namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 表示一个浙大校友令牌的详细信息。
/// </summary>
public class ZuaaTokenDetailInfo
{
	/// <summary>
	/// 该令牌关联到的姓名。
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// 该令牌关联到的电话号码。
	/// </summary>
	public required string Phone { get; set; }


	/// <summary>
	/// 该令牌的身份类型。目前 3 表示校友，其它值未使用。
	/// </summary>
	public int Level { get; set; }

	/// <summary>
	/// 令牌值。
	/// </summary>
	public required string Token { get; set; }
}