using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
/// 提供对 <see cref="ExcludeFromMigrationsAttribute"/> 的实现。
/// </summary>
public class ExcludeFromMigrationsAttributeConvention : EntityTypeAttributeConventionBase<ExcludeFromMigrationsAttribute>
{
	/// <inheritdoc />
	public ExcludeFromMigrationsAttributeConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
	{
	}

	/// <inheritdoc />
	protected override void ProcessEntityTypeAdded(IConventionEntityTypeBuilder entityTypeBuilder, ExcludeFromMigrationsAttribute attribute,
		IConventionContext<IConventionEntityTypeBuilder> context)
	{
		entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
	}
}