namespace TuDog.Interfaces;

/// <summary>
/// 日志接口
/// </summary>
public interface ILogger
{
    /// <summary>
    /// 记录信息日志
    /// </summary>
    /// <param name="message">日志信息</param>
    void LogInformation(string message);

    /// <summary>
    /// 记录警告日志
    /// </summary>
    /// <param name="message">日志信息</param>
    void LogWarning(string message);

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="message">日志信息</param>
    void LogError(string message);

    /// <summary>
    /// 记录调试日志
    /// </summary>
    /// <param name="message">日志信息</param>
    void LogDebug(string message);
}