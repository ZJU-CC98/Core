using Microsoft.Extensions.DependencyInjection;

namespace CC98.Services.ContentCheck;

/// <summary>
/// 为内容审查服务配置提供快捷方法。
/// </summary>
public sealed class ContentCheckServiceBuilder
{
	/// <summary>
	/// 初始化 <see cref="ContentCheckServiceBuilder"/> 对象的新实例。
	/// </summary>
	/// <param name="serviceCollection">服务容器对象。</param>
	internal ContentCheckServiceBuilder(IServiceCollection serviceCollection)
	{
		ServiceCollection = serviceCollection;
	}

	/// <summary>
	/// 服务容器。
	/// </summary>
	public IServiceCollection ServiceCollection { get; }

	/// <summary>
	/// 添加给定类型的内容审查服务提供程序。
	/// </summary>
	/// <typeparam name="T">要添加的内容审查程序。</typeparam>
	/// <returns></returns>
	public ContentCheckServiceBuilder AddServiceProvider<T>()
		where T : IContentCheckServiceProvider
	{
		ServiceCollection.Configure<ContentCheckOptions>(options => { options.ServiceProviders.Add(T.Name, typeof(T)); });
		return this;
	}
}