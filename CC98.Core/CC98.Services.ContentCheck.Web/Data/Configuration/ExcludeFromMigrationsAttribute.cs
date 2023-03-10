using System;

namespace CC98.Services.ContentCheck.Data.Configuration;

/// <summary>
/// 表示在执行数据库迁移时将忽略该实体类型。
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ExcludeFromMigrationsAttribute : Attribute
{
}