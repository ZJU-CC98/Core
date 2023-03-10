using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CC98.Services.ContentCheck.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示对单项内容的审核结果。
/// </summary>
[Index(nameof(Time), IsDescending = new[] { true })]
[Index(nameof(Type), nameof(Time), IsDescending = new[] { false, true })]
public abstract class ContentCheckItem
{
	/// <summary>
	/// 获取或设置该记录的标识。
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	/// 获取或设置该记录的类型。
	/// </summary>
	[Discriminator(ContentCheckItemType.Post, typeof(PostContentCheckItem))]
	[Discriminator(ContentCheckItemType.File, typeof(FileContentCheckItem))]
	public ContentCheckItemType Type { get; set; }

	/// <summary>
	/// 获取或设置审核的发起时间。
	/// </summary>
	public DateTimeOffset Time { get; set; }

	/// <summary>
	/// 获取或设置被检查的内容类型。
	/// </summary>
	public ContentCheckServiceType CheckType { get; set; }

	/// <summary>
	/// 获取或设置审核的最终结果。
	/// </summary>
	public ContentCheckResult Result { get; set; }

	/// <summary>
	/// 获取或设置该记录对应的所有操作记录的集合。
	/// </summary>
	[InverseProperty(nameof(ContentCheckOperationRecord.Item))]
	public virtual IList<ContentCheckOperationRecord> OperationRecords { get; set; } =
		new Collection<ContentCheckOperationRecord>();
}