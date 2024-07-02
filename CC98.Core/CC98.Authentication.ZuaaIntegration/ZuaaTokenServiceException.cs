namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 定义 <see cref="ZuaaTokenService"/> 运行期间发生的异常。
/// </summary>
public class ZuaaTokenServiceException : Exception
{
	/// <summary>
	/// 初始化 <see cref="ZuaaTokenServiceException"/> 类型的新实例。
	/// </summary>
	public ZuaaTokenServiceException()
	{
	}

	/// <summary>
	/// 用指定的消息初始化 <see cref="ZuaaTokenServiceException"/> 类型的新实例。
	/// </summary>
	/// <param name="message">异常的错误描述。</param>
	public ZuaaTokenServiceException(string message) : base(message)
	{
	}

	/// <summary>
	/// 用户指定的消息和内部异常初始化 <see cref="ZuaaTokenServiceException"/> 类型的新实例。
	/// </summary>
	/// <param name="message">异常的错误描述。</param>
	/// <param name="inner">引发该异常的内部异常。</param>
	public ZuaaTokenServiceException(string message, Exception inner) : base(message, inner)
	{
	}
}