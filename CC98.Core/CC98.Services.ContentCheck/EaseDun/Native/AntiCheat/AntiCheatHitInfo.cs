using System.Text.Json.Serialization;

namespace CC98.Services.ContentCheck.EaseDun.Native.AntiCheat;

/// <summary>
/// 定义文本反作弊的命中信息。
/// </summary>
public class AntiCheatHitInfo
{
    /// <summary>
    /// 命中的类型。
    /// </summary>
    [JsonPropertyName("hitType")]
    public AntiCheatHitType Type { get; set; }

    /// <summary>
    /// 命中类型对应的详情。
    /// </summary>
    [JsonPropertyName("HitMessage")]
    public required string Message { get; set; }
}