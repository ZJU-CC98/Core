using System.Collections.Generic;

namespace CC98.Services.ContentCheck.EaseDun.Native.Images;

/// <summary>
/// 定义图片的安全检测结果。
/// </summary>
public class ImageAntiSpamInfo : IAntiSpamInfo
{
	/// <summary>
	/// 本次检测的任务标识。由网易易盾服务后台生成。
	/// </summary>
	public required string TaskId { get; set; }

	/// <summary>
	/// 检测的图片名称。由 <see cref="ImageItem.Name"/> 定义。
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// 检测的数据标识。由 <see cref="ImageItem.DataId"/> 定义。
	/// </summary>
	public string? DataId { get; set; }

	/// <summary>
	/// 本次检测操作的结果状态。
	/// </summary>
	public required ImageDetectStatus Status { get; set; }

	/// <summary>
	/// 当 <see cref="Status"/> 为 <see cref="ImageDetectStatus.Failed"/> 时，指示操作失败的原因。
	/// </summary>
	public ImageFailureReason? FailureReason { get; set; }

	/// <summary>
	/// 本次检测的建议处理方式。
	/// </summary>
	public required ItemResultSuggestion Suggestion { get; set; }

	/// <summary>
	/// 本次检测的审查模式。
	/// </summary>
	public required ItemCensorType CensorType { get; set; }

	/// <summary>
	/// 当 <see cref="Suggestion"/> 为 <see cref="ItemResultSuggestion.Suspect"/> 时，指示内容的嫌疑级别。该属性默认不返回，需后台开启后方可使用。
	/// </summary>
	public ItemResultSuggestionLevel? SuggestionLevel { get; set; }

	/// <summary>
	/// 本次检测的结果类型。
	/// </summary>
	public required ItemResultType ResultType { get; set; }

	/// <summary>
	/// 对于长图或者动图，返回图片分拆后的帧数。对于其他图片，默认返回 <c>1</c>。
	/// </summary>
	public required int FrameSize { get; set; }

	/// <summary>
	/// 图片文件中是否包含隐藏数据。该功能需要后台开启后方可使用。
	/// </summary>
	public bool? Hidden { get; set; }

	/// <summary>
	/// 当 <see cref="Hidden"/> 为 <c>true</c> 时，指示隐藏数据的格式。该功能需要后台开启后方可使用。
	/// </summary>
	public string? HiddenFormat { get; set; }

	/// <summary>
	/// 图片检测的命中结果信息。
	/// </summary>
	public required ImageHitLabel[] Labels { get; set; }

	/// <summary>
	/// 对 <see cref="IAntiSpamInfo.Labels"/> 的实现。
	/// </summary>
	IEnumerable<IHitLabel> IAntiSpamInfo.Labels => Labels;
}