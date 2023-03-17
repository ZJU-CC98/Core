using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
///     表示对单项内容的审核结果。
/// </summary>
[Index(nameof(Time), IsDescending = new[] { true })]
[Index(nameof(Type), nameof(Time), IsDescending = new[] { false, true })]
internal abstract class ContentCheckItem
{
	/// <summary>
	///     获取或设置该记录的标识。
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	///     获取或设置该记录的类型。
	/// </summary>
	public ContentCheckItemType Type { get; set; }

	/// <summary>
	///     获取或设置审核的发起时间。
	/// </summary>
	public DateTimeOffset Time { get; set; }

	/// <summary>
	///     获取或设置被检查的内容类型。
	/// </summary>
	public ContentCheckServiceType CheckType { get; set; }

	/// <summary>
	///     获取或设置审核的最终结果。
	/// </summary>
	[DefaultValue(ContentCheckResult.Pass)]
	public ContentCheckResult Result { get; set; }

	/// <summary>
	/// 获取或设置一个值，指示该对象最近的更新是否已经被人工复核。
	/// </summary>
	[DefaultValue(false)]
	public bool IsReviewed { get; set; }

	/// <summary>
	///     获取或设置该记录对应的所有操作记录的集合。
	/// </summary>
	[InverseProperty(nameof(ContentCheckOperationRecord.Item))]
	public virtual IList<ContentCheckOperationRecord> OperationRecords { get; set; } =
		new Collection<ContentCheckOperationRecord>();
}