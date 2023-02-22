namespace CC98.Services.ContentCheck.EaseDun.Native;

/// <summary>
///     定义消息发送者和接收者之间的关系。
/// </summary>
public enum UserRelationship
{
	/// <summary>
	///     关系未知。
	/// </summary>
	Unknown = 0,

	/// <summary>
	///     接收者已关注发送者。
	/// </summary>
	ReceiverFollowedSender,

	/// <summary>
	///     发送者已关注接收者。
	/// </summary>
	SenderFollowedReceiver,

	/// <summary>
	///     发送者和接收者相互关注。
	/// </summary>
	Followed,

	/// <summary>
	///     发送者和接收者均未关注对方。
	/// </summary>
	NotFollowed
}