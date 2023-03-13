using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
///     提供对 <see cref="DefaultValueAttribute" /> 的实现。
/// </summary>
public class DefaultValueAttributeConvention : PropertyAttributeConventionBase<DefaultValueAttribute>
{
	/// <inheritdoc />
	public DefaultValueAttributeConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
	{
	}

	/// <inheritdoc />
	protected override void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder,
		DefaultValueAttribute attribute,
		MemberInfo clrMember, IConventionContext context)
	{
		propertyBuilder.Metadata.SetDefaultValue(attribute.Value);
	}
}