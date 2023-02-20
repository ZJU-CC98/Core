// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;

using Sakura.Text.Json.JsonFlattener.Core;

Console.WriteLine("Hello, World!");



public partial class Test
{
	public int Data { get; set; }

	[JsonFlatten]
	[JsonIgnore]
	public Test2 Test2 { get; set; } = new Test2();

	public void TestMethod()
	{
	}
}

public class Test2
{
	public int Data2 { get; set; }
	public string Data3 { get; set; } = null!;
}