namespace CC98.Services.ContentCheck.EaseDun.Native.AntiCheat;

/// <summary>
/// 定义文本反作弊检测的检测结果。
/// </summary>
public enum AntiCheatSuggestion
{
    /// <summary>
    /// 通过。
    /// </summary>
    Passed = 0,
    /// <summary>
    /// 嫌疑。
    /// </summary>
    Suspicion = 10,
    /// <summary>
    /// 不通过。
    /// </summary>
    Failed = 20
}