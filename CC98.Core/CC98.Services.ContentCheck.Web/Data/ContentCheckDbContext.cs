using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
///     表示内容审核数据库必须提供的功能支持。
/// </summary>
internal class ContentCheckDbContext(DbContextOptions<ContentCheckDbContext> options) : DbContext(options)
{
	/// <summary>
	///     获取或设置审核结果的集合。
	/// </summary>
	public virtual required DbSet<ContentCheckItem> ContentCheckItems { get; set; }

	/// <summary>
	///     获取或设置审核操作记录的集合。
	/// </summary>
	public virtual required DbSet<ContentCheckOperationRecord> ContentCheckOperationRecords { get; set; }

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ContentCheckItem>()
			.HasDiscriminator(p => p.Type)
			.HasValue<PostContentCheckItem>(ContentCheckItemType.Post)
			.HasValue<FileContentCheckItem>(ContentCheckItemType.File);
	}
}