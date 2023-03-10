using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示自动审查系统的调用记录。
/// </summary>
public class ContentCheckOperationRecord
{
	/// <summary>
	/// 获取或设置该对象的标识。
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	/// 获取或设置该记录对应的结果记录的标识。
	/// </summary>
	public int ItemId { get; set; }

	/// <summary>
	/// 获取或设置该记录对应的结果记录。
	/// </summary>
	[ForeignKey(nameof(ItemId))]
	public virtual ContentCheckItem Item { get; set; } = null!;

	/// <summary>
	/// 审核的发起类型。
	/// </summary>
	[DefaultValue(ContentCheckOperationType.Auto)]
	public ContentCheckOperationType Type { get; set; } = ContentCheckOperationType.Auto;

	/// <summary>
	/// 获取或设置审查发起者。如为系统自动操作，则该属性为 <c>null</c>。
	/// </summary>
	public int? OperatorId { get; set; }

	/// <summary>
	/// 获取或设置本次审查使用的审查服务。
	/// </summary>
	[Required]
	public required string ServiceProvider { get; set; }

	/// <summary>
	/// 获取或设置审查服务的使用类型。
	/// </summary>
	public ContentCheckServiceType ServiceType { get; set; }

	/// <summary>
	/// 获取或设置审查提供的额外参数。
	/// </summary>
	public string? RequestData { get; set; }

	/// <summary>
	/// 获取或设置审查的完整响应。
	/// </summary>
	public string? Response { get; set; }

	/// <summary>
	/// 获取或设置本次操作的时间。
	/// </summary>
	public required DateTimeOffset Time { get; set; }

	/// <summary>
	/// 获取或设置审查的结果。
	/// </summary>
	public ContentCheckResultType Result { get; set; }
}