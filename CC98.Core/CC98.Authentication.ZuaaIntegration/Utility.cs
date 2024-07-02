using System.Reflection;

namespace CC98.Authentication.ZuaaIntegration;

/// <summary>
/// 提供辅助方法。该类型为静态类型。
/// </summary>
internal static class Utility
{
	/// <summary>
	/// 将对象转换为基于属性名称和值的字典形式。
	/// </summary>
	/// <param name="obj">要转换的对象。</param>
	/// <returns>转换后的字典。其中每个项目的键为 <paramref name="obj"/> 的属性名称，值为该属性对应的值。</returns>
	public static Dictionary<string, object?> ObjectToDictionary(this object? obj)
	{
		if (obj == null)
		{
			return [];
		}

		return
			(from prop in obj.GetType().GetTypeInfo().DeclaredProperties
				let key = prop.Name
				let value = prop.GetValue(obj)
				select (key, value))
			.ToDictionary();
	}
}