using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
/// 提供对 <see cref="DiscriminatorAttribute"/> 的实现支持。
/// </summary>
public class DiscriminatorAttributeConvention : PropertyAttributeConventionBase<DiscriminatorAttribute>
{
	/// <inheritdoc />
	public DiscriminatorAttributeConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
	{
	}

	/// <inheritdoc />
	protected override void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, DiscriminatorAttribute attribute,
		MemberInfo clrMember, IConventionContext context)
	{
		var entityType = propertyBuilder.Metadata.DeclaringEntityType.Builder;

		var mappingType = propertyBuilder.ModelBuilder.Entity(attribute.Type);

		if (mappingType == null)
		{
			throw new InvalidOperationException($"CLR 类型 {nameof(attribute.Type)} 未映射为实体类型。");
		}

		entityType.HasDiscriminator(clrMember)!
			.HasValue(mappingType.Metadata, attribute.Value);

	}
}