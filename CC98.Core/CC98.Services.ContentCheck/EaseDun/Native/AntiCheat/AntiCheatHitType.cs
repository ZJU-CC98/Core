namespace CC98.Services.ContentCheck.EaseDun.Native.AntiCheat;

/// <summary>
/// 定义反作弊的命中类型。
/// </summary>
public enum AntiCheatHitType
{
    /// <summary>
    /// 未检测到作弊。
    /// </summary>
    Ok = 0,
    /// <summary>
    /// 数据异常，主要表现有数据完整性校验不通过或数据伪造等。
    /// </summary>
    DataError,
    /// <summary>
    /// 行为异常，主要表现有用户的操作行为（鼠标点击/移动等）无法通过行为验证模型等。
    /// </summary>
    BehaviorError,
    /// <summary>
    /// 设备模型，主要表现有设备指纹等信息无法通过设备验证模型等。
    /// </summary>
    DeviceModel,
    /// <summary>
    /// 业务模型，主要表现有撞库、批量操作、违反业务规则等。
    /// </summary>
    BusinessModel,
    /// <summary>
    /// 校验异常，主要表现有数据强校验结果异常或数据伪造等。
    /// </summary>
    CheckError,
    /// <summary>
    /// 模拟器，主要表现有安卓端使用手机模拟器的行为。
    /// </summary>
    Simulator,
    /// <summary>
    /// 越狱或ROOT，主要表现有 iOS 系统已越狱或 Android 系统已 root。
    /// </summary>
    JailBreak,
    /// <summary>
    /// 浏览器异常，主要表现有浏览器分辨率等参数异常或遭篡改等。
    /// </summary>
    BrowserError,
    /// <summary>
    /// IP 异常，主要表现有终端 IP 画像结果为风险 IP 或高危 IP 等。
    /// </summary>
    IpError,
    /// <summary>
    /// 黑名单，易盾自有及客户自定义黑名单数据。
    /// </summary>
    BlackList,
    /// <summary>
    /// 白名单，易盾自有及客户自定义白名单数据。
    /// </summary>
    WhiteList,
    /// <summary>
    /// 高危账号，主要表现有团伙账号或异常共享账号等风险账号类型。
    /// </summary>
    HighRiskAccount,
    /// <summary>
    /// 多开小号，主要表现有批量多开。
    /// </summary>
    BatchAccounts,
    /// <summary>
    /// 篡改硬件信息，主要表现为篡改硬件设备参数信息。
    /// </summary>
    HardwareInfoTampering,
    /// <summary>
    /// 篡改系统信息，主要表现为篡改系统参数信息。
    /// </summary>
    SystemInfoTampering,
    /// <summary>
    /// 高危设备，主要表现为高危设备画像风险评分，黑产特征设备等类型。
    /// </summary>
    HighRiskDevice,
    /// <summary>
    /// 群控或云控，主要表现为群控工作室设备或云机。
    /// </summary>
    GroupControl,
    /// <summary>
    /// 安装修改工具，主要表现有安装 Hook 修改、Xposed 修改，Magisk 修改等。
    /// </summary>
    SetupTemperTools,
    /// <summary>
    /// 虚拟环境，非真实设备访问环境，区别于安卓模拟器，如编辑后台等。
    /// </summary>
    VirtualizedEnvironment,
    /// <summary>
    /// 脚本工具，黑灰产用于作弊行为的脚本工具。
    /// </summary>
    ScriptTool
}