using System.Threading;
using System.Threading.Tasks;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义内容审查服务必须提供的基本操作。
/// </summary>
public interface IContentCheckServiceProvider
{
	/// <summary>
	///     获取该服务的名称。
	/// </summary>
	public static abstract string Name { get; }

	/// <summary>
	///     按照默认设置执行对用户发言的审查。
	/// </summary>
	/// <param name="post">要审查的发言对象。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。 操作结果为审查结果。如果本次内容未进行审查，则返回 <c>null</c>。</returns>
	Task<ContentCheckServiceExecutionResult?> ExecutePostCheckAsync(IUserPost post,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     按照默认设置执行对用户文件的审查。
	/// </summary>
	/// <param name="file">要审查的文件。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。 操作结果为审查结果。如果本次内容未进行审查，则返回 <c>null</c>。</returns>
	Task<ContentCheckServiceExecutionResult?> ExecuteFileCheckAsync(IUserFile file,
		CancellationToken cancellationToken = default);
}