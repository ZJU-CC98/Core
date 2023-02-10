using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC98;

/// <summary>
/// 表示类型提供一个默认值。
/// </summary>
/// <typeparam name="T">默认值的类型。</typeparam>
public interface IAppSettingWithDefaultValue<out T>
{
	/// <summary>
	/// 获取该类型提供的默认值。
	/// </summary>
	public static abstract T Default { get; }
}