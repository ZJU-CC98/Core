using System.Collections.Generic;
using CC98.Services.ContentCheck;
using CC98.Services.ContentCheck.EaseDun;
using JetBrains.Annotations;

namespace CC98.Management.Services.EaseDun;

/// <summary>
///     网易易盾配置服务。
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
public class EaseDunOptions
{
	/// <summary>
	///     网易易盾的客户标识。
	/// </summary>
	public required string SecretId { get; set; }

	/// <summary>
	///     网易易盾的客户机密。
	/// </summary>
	public required string SecretKey { get; set; }


	/// <summary>
	///     网易易盾查询配置的查询标签。
	/// </summary>
	public required CheckLabel[] CheckLabels { get; set; }

	/// <summary>
	///     网易易盾提供的命中子标签。
	/// </summary>
	public required HitSubCategory[] HitSubCategories { get; set; }

	/// <summary>
	/// 网易易盾的服务设置。
	/// </summary>
	public required Dictionary<ContentCheckServiceType, EaseDunServiceItem> Services { get; set; }
}