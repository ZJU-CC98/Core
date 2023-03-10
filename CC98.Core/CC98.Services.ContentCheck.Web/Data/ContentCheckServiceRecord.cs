using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示调用内容审核服务的记录的集合。
/// </summary>
[Index(nameof(Time), IsDescending = new[] { true })]
public class ContentCheckServiceRecord
{
	/// <summary>
	/// 获取或设置该对象的标识。
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	/// 获取或设置该调用操作发生的时间。
	/// </summary>
	public DateTimeOffset Time { get; set; }
}