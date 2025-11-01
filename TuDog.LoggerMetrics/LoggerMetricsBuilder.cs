using DryIoc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using Serilog.Sinks.SystemConsole.Themes;

public static class LoggerMetricsBuilder
{
    /// <summary>
    /// 添加日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="lokiUrl">loki服务地址，如果为空那么将不会启用</param>
    /// <param name="lokiLabels">loki追加的数据</param>
    /// <param name="logDirectory">本地日志存放路径，精确到路径。文件默认是按照log-日期.txt每日创建新文件</param>
    public static void AddLoggerBuilder(this IContainer services, string? lokiUrl, IEnumerable<LokiLabel>? lokiLabels,
        string logDirectory)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);

        var logPath = Path.Combine(logDirectory, "log-.txt");

        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day);

        if (!string.IsNullOrEmpty(lokiUrl))
            logger = logger.WriteTo.GrafanaLoki(lokiUrl, lokiLabels);
        var createdLogger = logger.CreateLogger();

        // services.AddLogging(logging =>
        // {
        //     logging.ClearProviders();
        //     logging.AddSerilog(createdLogger);
        // });
    }
}