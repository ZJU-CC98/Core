using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC98.Services.ContentCheck.EaseDun;

/// <summary>
/// 为网易易盾服务提供常量。该类型为静态类型。
/// </summary>
public static class EaseDunConstants
{
	/// <summary>
	/// 获取网易易盾服务的根 URL 地址。改地址将作为其他服务地址的相对基础路径。
	/// </summary>
	public const string ServiceBaseUri = "http://as.dun.163.com/v5/";

	/// <summary>
	/// 获取网易易盾文本单次检测服务的 URL 相对于 <see cref="ServiceBaseUri"/> 的相对地址。
	/// </summary>
	public const string TextCheckUri = "text/check";


	/// <summary>
	/// 获取网易易盾文本批量检测服务的 URL 相对于 <see cref="ServiceBaseUri"/> 的相对地址。该地址由 <see cref="EaseDunService.BatchCheckTextAsync"/> 方法使用。
	/// </summary>
	public const string TextBatchCheckUri = "text/batch-check";

	/// <summary>
	/// 获取网易易盾文本图像检测服务的 URL 相对于 <see cref="ServiceBaseUri"/> 的相对地址。该地址由 <see cref="EaseDunService.CheckImageAsync"/> 方法使用。
	/// </summary>
	public const string ImageCheckUri = "image/check";
}