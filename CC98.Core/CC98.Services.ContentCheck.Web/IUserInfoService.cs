using System.Threading;
using System.Threading.Tasks;

namespace CC98.Services.ContentCheck;

/// <summary>
///     定义提供用户信息的服务。
/// </summary>
public interface IUserInfoService
{
	/// <summary>
	///     获取给定用户的信息。
	/// </summary>
	/// <param name="userId">要获取信息的用户的标识。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。操作结果包含用户信息。如果结果为 <c>null</c>，表示给定的用户信息不存在。</returns>
	Task<IUserInfo?> GetUserInfoAsync(int userId, CancellationToken cancellationToken = default);
}