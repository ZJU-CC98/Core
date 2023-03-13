using System;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
///     设定在实体框架 TPH 模式中区分派生类时属性值和派生类型的对应关系。要涵盖所有派生情况，请为单个实体属性多次设置该特性。
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class DiscriminatorAttribute : Attribute
{
	/// <summary>
	///     初始化 <see cref="DiscriminatorAttribute" /> 类型的新实例。
	/// </summary>
	/// <param name="value">该实体属性可能设置的值。</param>
	/// <param name="type">在该实体属性的值设置为 <paramref name="value" /> 时，该实例将对应的派生类型。</param>
	public DiscriminatorAttribute(object value, Type type)
	{
		Value = value;
		Type = type;
	}

	/// <summary>
	///     该实体属性可能设置的值。
	/// </summary>
	public object Value { get; }

	/// <summary>
	///     在该实体属性的值设置为 <see cref="Value" /> 时，该实例将对应的派生类型。
	/// </summary>
	public Type Type { get; }
}