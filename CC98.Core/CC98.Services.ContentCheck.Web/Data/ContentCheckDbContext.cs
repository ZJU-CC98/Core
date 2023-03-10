using System;
using System.Threading;
using System.Threading.Tasks;
using CC98.Services.ContentCheck.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CC98.Services.ContentCheck.Data;

/// <summary>
/// 表示内容审核数据库必须提供的功能支持。
/// </summary>
internal class ContentCheckDbContext : DbContext
{
    /// <summary>
    /// 获取或设置审核结果的集合。
    /// </summary>
    public virtual required DbSet<ContentCheckItem> ContentCheckItems { get; set; }

    /// <summary>
    ///获取或设置审核操作记录的集合。
    /// </summary>
    public virtual required DbSet<ContentCheckOperationRecord> ContentCheckOperationRecords { get; set; }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // 辅助方法
        static ProviderConventionSetBuilderDependencies GetDependencies(IServiceProvider sp)
        {
            return sp.GetRequiredService<ProviderConventionSetBuilderDependencies>();
        }

        base.ConfigureConventions(configurationBuilder);

        // 添加自定义转换
        configurationBuilder.Conventions.Add(sp => new ExcludeFromMigrationsAttributeConvention(GetDependencies(sp)));
        configurationBuilder.Conventions.Add(sp => new DefaultValueAttributeConvention(GetDependencies(sp)));
        configurationBuilder.Conventions.Add(sp => new DiscriminatorAttributeConvention(GetDependencies(sp)));
    }
}