﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CC98.Services.ContentCheck.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 提供内容审查服务的核心实现。
/// </summary>
public class ContentCheckService
{
	/// <summary>
	/// 初始化 <see cref="ContentCheckService"/> 对象的新实例。
	/// </summary>
	/// <param name="serviceScopeFactory"><see cref="IServiceScopeFactory"/> 服务对象。</param>
	/// <param name="options"><see cref="IOptions{ContentCheckOptions}"/> 服务对象。</param>
	/// <param name="contentCheckSettingService"><see cref="AppSettingService{ContentCheckSystemSetting}"/> 服务对象。</param>
	public ContentCheckService(IServiceScopeFactory serviceScopeFactory, IOptions<ContentCheckOptions> options,
		AppSettingService<ContentCheckSystemSetting> contentCheckSettingService)
	{
		ContentCheckSettingService = contentCheckSettingService;
		ServiceScopeFactory = serviceScopeFactory;
		Options = options.Value;
	}

	/// <summary>
	/// 内容审查服务程序的配置信息。
	/// </summary>
	private ContentCheckOptions Options { get; }

	private IServiceScopeFactory ServiceScopeFactory { get; }

	/// <summary>
	/// 获取内容审查服务的系统设置数据。
	/// </summary>
	private AppSettingService<ContentCheckSystemSetting> ContentCheckSettingService { get; }

	/// <summary>
	/// 获取内容审核服务的全局设置。
	/// </summary>
	private ContentCheckSetting GlobalSetting => ContentCheckSettingService.Current.Global;

	/// <summary>
	/// 尝试获取目前系统中注册的默认内容审查服务程序。
	/// </summary>
	/// <returns>系统中注册的默认内容审查服务程序实例。</returns>
	/// <exception cref="InvalidOperationException">系统未注册默认的内容审查服务程序，或者注册信息无效，或该程序无法正常初始化。</exception>
	private IContentCheckServiceProvider GetDefaultServiceProvider(IServiceProvider serviceProvider)
		=> GetServiceProviderByName(serviceProvider, GlobalSetting.ServiceProvider);

	/// <summary>
	/// 尝试获取目前系统中注册的默认内容审查服务程序。
	/// </summary>
	/// <returns>系统中注册的默认内容审查服务程序实例。</returns>
	/// <exception cref="InvalidOperationException">系统未注册默认的内容审查服务程序，或者注册信息无效，或该程序无法正常初始化。</exception>
	private IContentCheckServiceProvider GetServiceProviderByName(IServiceProvider serviceProvider, string contentCheckServiceProvider)
	{
		if (Options.ServiceProviders.TryGetValue(contentCheckServiceProvider, out var result))
		{
			return (IContentCheckServiceProvider)serviceProvider.GetRequiredService(result);
		}

		throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture,
			"无法获取名称为 {0} 的服务，该服务未注册或无法创建。", contentCheckServiceProvider));
	}

	/// <summary>
	/// 使用默认的审查服务执行发言审查操作。
	/// </summary>
	/// <param name="item">要检查的发言信息。</param>
	/// <param name="recordMode">本次结果的记录模式。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。</returns>
	public async Task<ContentCheckServiceExecutionResult?> CheckPostAsync(IUserPost item,
		ContentCheckRecordMode recordMode = ContentCheckRecordMode.Default,
		CancellationToken cancellationToken = default)
	{
		await using var scope = ServiceScopeFactory.CreateAsyncScope();

		var provider = GetDefaultServiceProvider(scope.ServiceProvider);
		var dbContext = scope.ServiceProvider.GetRequiredService<ContentCheckDbContext>();

		var result = await provider.ExecutePostCheckAsync(item, cancellationToken);

		// 未产生结果。则不进行任何操作。
		if (result == null)
		{
			return null;
		}

		// 判定是否需要记录结果。
		var shouldRecord = recordMode switch
		{
			ContentCheckRecordMode.Always => true,
			ContentCheckRecordMode.Never => false,
			_ => (int)result.Result >= (int)GlobalSetting.RecordLevel
		};

		if (shouldRecord)
		{
			// 获取内容记录
			var recordItem =
				await (from i in dbContext.ContentCheckItems.OfType<PostContentCheckItem>().AsNoTracking()
					   where i.PostId == item.Id
					   select i).SingleOrDefaultAsync(cancellationToken);

			// 如果没有现存记录则添加新记录。
			if (recordItem == null)
			{
				recordItem = new()
				{
					Type = ContentCheckItemType.Post,
					CheckType = ContentCheckServiceType.Text,
					PostId = item.Id,
				};

				dbContext.ContentCheckItems.Add(recordItem);
			}

			// 修改该项目的记录结果和时间为最新结果。
			recordItem.Result = result.Result;
			recordItem.Time = DateTimeOffset.Now;

			// 服务调用记录。
			var operationItem = new ContentCheckOperationRecord
			{
				Type = ContentCheckOperationType.Auto,
				ServiceProvider = GlobalSetting.ServiceProvider,
				ServiceType = ContentCheckServiceType.Text,
				Result = result.Result,
				RequestData = result.Request,
				Response = result.Response,
				OperatorId = null,
				Item = recordItem,
				Time = recordItem.Time,
			};

			dbContext.ContentCheckOperationRecords.Add(operationItem);

			await dbContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
}