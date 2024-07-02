namespace CC98.Services.ContentCheck.EaseDun.Native.AntiCheat;

/// <summary>
/// 表示文本反作弊检测结果的详细信息。
/// </summary>
public class AntiCheatDetail
{
    /// <summary>
    /// 反作弊检测的结果。
    /// </summary>
    public AntiCheatSuggestion Suggestion { get; set; }

    /// <summary>
    /// 反作弊检测的的命中信息的集合。
    /// </summary>
    public required AntiCheatHitInfo[] HitInfos { get; set; }
}