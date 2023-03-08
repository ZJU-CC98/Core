using System;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
///     表示网易易盾服务相关功能在运行时发生的错误。
/// </summary>
public class EaseDunServiceException : Exception
{
    /// <summary>
    ///     初始化 <see cref="EaseDunServiceException" /> 对象的新实例。
    /// </summary>
    public EaseDunServiceException()
    {
    }

    /// <summary>
    ///     用指定的错误消息初始化 <see cref="EaseDunServiceException" /> 对象的新实例。
    /// </summary>
    /// <param name="message">用于描述错误的消息。</param>
    public EaseDunServiceException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     用指定的错误消息和内部异常初始化 <see cref="EaseDunServiceException" /> 对象的新实例。
    /// </summary>
    /// <param name="message">用于描述错误的消息。</param>
    /// <param name="inner">引发该异常的内部异常。</param>
    public EaseDunServiceException(string message, Exception inner)
        : base(message, inner)
    {
    }
}