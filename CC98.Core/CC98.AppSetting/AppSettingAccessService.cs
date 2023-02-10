using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CC98;

/// <summary>
///     为应用程序设置功能提供访问接口。
/// </summary>
public class AppSettingAccessService
{
	/// <summary>
	///     初始化一个应用程序设置工具的新实例。
	/// </summary>
	/// <param name="scopeFactory">提供区域的创建操作。</param>
	/// <param name="options">应用设置对象。</param>
	/// <param name="serializationService">数据的序列化帮助程序。</param>
	public AppSettingAccessService(IServiceScopeFactory scopeFactory, IOptions<AppSettingAccessOptions> options,
		IDataSerializationService serializationService)
	{
		ScopeFactory = scopeFactory;
		Options = options.Value;
		SerializationService = serializationService;
	}

	/// <summary>
	///     服务域工厂对象。
	/// </summary>
	private IServiceScopeFactory ScopeFactory { get; }

	/// <summary>
	///     获取设置对象。
	/// </summary>
	private AppSettingAccessOptions Options { get; }

	/// <summary>
	///     数据编解码辅助工具。
	/// </summary>
	private IDataSerializationService SerializationService { get; }

	/// <summary>
	///     获取应用设置对象。
	/// </summary>
	/// <param name="dbContext">数据库服务对象。</param>
	/// <returns>当前应用的设置对象。如果该对象不存在则返回 null。</returns>
	private AppSetting? LoadSettingItem(CC98V2DbContext dbContext)
	{
		return (from i in dbContext.AppSettings
				where i.AppName == Options.AppName
				select i).SingleOrDefault();
	}

	/// <summary>
	///     获取应用设置对象。
	/// </summary>
	/// <param name="dbContext">数据库服务对象。</param>
	/// <param name="cancellationToken"></param>
	/// <returns>当前应用的设置对象。如果该对象不存在则返回 null。</returns>
	private Task<AppSetting?> LoadSettingItemAsync(CC98V2DbContext dbContext, CancellationToken cancellationToken = default)
	{
		return (from i in dbContext.AppSettings
				where i.AppName == Options.AppName
				select i).SingleOrDefaultAsync(cancellationToken);
	}

	/// <summary>
	///     加载应用程序设置数据。
	/// </summary>
	/// <typeparam name="T">应用程序设置数据的类型。</typeparam>
	/// <returns>加载的应用程序设置。如果该设置不存在，则返回 <typeparamref name="T" /> 类型的默认值。</returns>
	public T? LoadSetting<T>()
	{
		using var serviceScope = ScopeFactory.CreateScope();
		using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 加载设置
		var settingItem = LoadSettingItem(dbContext);

		// 如果设置项不存在则返回默认值，否则解码数据并返回
		return settingItem == null
			? default
			: SerializationService.Deserialize<T>(settingItem.Data, Options.DataFormat);
	}

	/// <summary>
	///     加载应用程序设置数据。
	/// </summary>
	/// <typeparam name="T">应用程序设置数据的类型。</typeparam>
	/// <returns>加载的应用程序设置。如果该设置不存在，则返回 <typeparamref name="T" /> 类型的默认值。</returns>
	public async Task<T?> LoadSettingAsync<T>()
	{
		using var serviceScope = ScopeFactory.CreateScope();
		await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 加载设置
		var settingItem = await LoadSettingItemAsync(dbContext);

		// 如果设置项不存在则返回默认值，否则解码数据并返回
		return settingItem == null
			? default
			: SerializationService.Deserialize<T>(settingItem.Data, Options.DataFormat);
	}

	/// <summary>
	///     写入应用程序设置。
	/// </summary>
	/// <typeparam name="T">应用程序设置数据的类型。</typeparam>
	/// <returns>表示异步操作的对象。</returns>
	public void SaveSetting<T>(T appSetting)
	{
		using var serviceScope = ScopeFactory.CreateScope();
		using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 设置数据
		var settingData = SerializationService.Serialize(appSetting, Options.DataFormat);

		// 加载现有设置
		var item = LoadSettingItem(dbContext);

		// 如果现有设置不存在，则创建新设置
		if (item == null)
			dbContext.AppSettings.Add(new()
			{
				Format = Options.DataFormat,
				AppName = Options.AppName,
				Data = settingData
			});
		else
			item.Data = settingData;

		dbContext.SaveChanges();
	}

	/// <summary>
	///     写入应用程序设置。
	/// </summary>
	/// <typeparam name="T">应用程序设置数据的类型。</typeparam>
	/// <param name="appSetting">要写入的应用程序设置。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的对象。</returns>
	public async Task SaveSettingAsync<T>(T appSetting, CancellationToken cancellationToken = default)
	{
		using var serviceScope = ScopeFactory.CreateScope();
		await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 设置数据
		var settingData = SerializationService.Serialize(appSetting, Options.DataFormat);

		// 加载现有设置
		var item = await LoadSettingItemAsync(dbContext, cancellationToken);

		// 如果现有设置不存在，则创建新设置
		if (item == null)
			dbContext.AppSettings.Add(new()
			{
				Format = Options.DataFormat,
				AppName = Options.AppName,
				Data = settingData
			});
		else
			item.Data = settingData;

		await dbContext.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	///     读取应用程序设置。如果应用程序设置不存在，则在数据库中写入默认值。
	/// </summary>
	/// <typeparam name="T">应用程序的数据数据类型。</typeparam>
	/// <param name="defaultValue">当数据不存在时使用的默认值。</param>
	/// <returns>当前数据库中的设置。如果设置不存在，则返回 <paramref name="defaultValue" />。</returns>
	public T LoadSettingOrDefault<T>(T defaultValue)
	{
		using var serviceScope = ScopeFactory.CreateScope();
		using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 加载当前设置项目
		var settingItem = LoadSettingItem(dbContext);

		// 如果设置存在，则读取内容
		if (settingItem != null)
			return SerializationService.Deserialize<T>(settingItem.Data, Options.DataFormat);
		// 否则写入新内容
		SaveSetting(defaultValue);
		return defaultValue;
	}

	/// <summary>
	///     读取应用程序设置。如果应用程序设置不存在，则在数据库中写入默认值。
	/// </summary>
	/// <typeparam name="T">应用程序的数据数据类型。</typeparam>
	/// <param name="defaultValue">当数据不存在时使用的默认值。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>当前数据库中的设置。如果设置不存在，则返回 <paramref name="defaultValue" />。</returns>
	public async Task<T> LoadSettingOrDefaultAsync<T>(T defaultValue, CancellationToken cancellationToken = default)
	{
		using var serviceScope = ScopeFactory.CreateScope();
		await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<CC98V2DbContext>();

		// 加载当前设置项目
		var settingItem = await LoadSettingItemAsync(dbContext, cancellationToken);

		// 如果设置存在，则读取内容
		if (settingItem != null)
			return SerializationService.Deserialize<T>(settingItem.Data, Options.DataFormat);
		// 否则写入新内容
		await SaveSettingAsync(defaultValue, cancellationToken);
		return defaultValue;
	}
}