using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace CC98;

/// <summary>
///     定义 CC98 V2 数据库结构。
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
internal class CC98V2DbContext : DbContext
{
	/// <summary>
	///     用给定的选项初始化一个数据库上下文对象。
	/// </summary>
	/// <param name="options">数据库上下文选项。</param>
	public CC98V2DbContext(DbContextOptions<CC98V2DbContext> options)
		: base(options)
	{
	}

	/// <summary>
	///     获取或设置数据库中包含的应用的信息。
	/// </summary>
	public virtual required DbSet<AppSetting> AppSettings { get; set; }
}