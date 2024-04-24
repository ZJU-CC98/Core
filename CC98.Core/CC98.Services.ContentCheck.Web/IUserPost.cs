using System;

namespace CC98.Services.ContentCheck;

/// <summary>
///     表示一个用户的发言信息。
/// </summary>
public interface IUserPost
{
	/// <summary>
	///     发言的标识。
	/// </summary>
	int Id { get; }

	/// <summary>
	///     发言的用户的标识。
	/// </summary>
	public int UserId { get; }

	/// <summary>
	///     发言的标题。如果为 <c>null</c> 则表示无标题。
	/// </summary>
	string? Title { get; }

	/// <summary>
	///     发言的内容。
	/// </summary>
	string Content { get; }

	/// <summary>
	///     发言的时间。
	/// </summary>
	DateTimeOffset Time { get; }

	/// <summary>
	///     本发言对应的主题的标识。
	/// </summary>
	int TopicId { get; }

	/// <summary>
	/// 本发言对应的版面的标识。
	/// </summary>
	int BoardId { get; }

	/// <summary>
	///     本发言对应的上级发言（引用）的标识。
	/// </summary>
	int? ParentId { get; }

	/// <summary>
	/// 本次发言的楼层。
	/// </summary>
	int Floor { get; }

	/// <summary>
	/// 本次发言是否为匿名。
	/// </summary>
	bool IsAnonymous { get; }

	/// <summary>
	///     本次发言操作的 IP 地址。
	/// </summary>
	string Ip { get; }
}