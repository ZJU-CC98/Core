using System;

namespace CC98.Services.ContentCheck;

/// <summary>
///     表示用户信息。
/// </summary>
public interface IUserInfo
{
	/// <summary>
	///     用户的标识。
	/// </summary>
	int Id { get; }

	/// <summary>
	///     用户的名称。
	/// </summary>
	string Name { get; }

	/// <summary>
	///     用户的生日。如果未定义生日则返回 <c>null</c>。
	/// </summary>
	DateOnly? BirthDay { get; }

	/// <summary>
	///     用户的性别。
	/// </summary>
	Gender? Gender { get; }

	/// <summary>
	///     用户的注册时间。
	/// </summary>
	DateTimeOffset RegisterTime { get; }

	/// <summary>
	///     用户的粉丝数。
	/// </summary>
	int FanCount { get; }

	/// <summary>
	///     用户的手机号码。
	/// </summary>
	string? PhoneNumber { get; }
}