using System;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
///     为实体属性指定默认值。
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DefaultValueAttribute : Attribute
{
	/// <summary>
	///     初始化 <see cref="DefaultValueAttribute" /> 的新实例。
	/// </summary>
	/// <param name="value">要使用的属性默认值。</param>
	public DefaultValueAttribute(object? value)
	{
		Value = value;
	}

	/// <summary>
	///     要设定的属性的默认值。
	/// </summary>
	public object? Value { get; }
}