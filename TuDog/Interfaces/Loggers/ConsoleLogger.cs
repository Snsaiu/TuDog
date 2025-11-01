using System.Diagnostics;
using TuDog.Enums;

namespace TuDog.Interfaces.Loggers;

internal sealed class ConsoleLogger(LogLevel logLevel) : ILogger
{
    private LogLevel _logLevel = logLevel;

    public void LogInformation(string message)
    {
        if (_logLevel < LogLevel.Info)
            return;
        var messagePackage = $"[INFO] {DateTime.Now:HH:mm:ss} {message}";
        if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(messagePackage);
            Console.ResetColor();
        }
        else
        {
            Trace.WriteLine(messagePackage);
        }
    }

    public void LogWarning(string message)
    {
        if (_logLevel < LogLevel.Warning)
            return;
        
        var messagePackage = $"[WARN] {DateTime.Now:HH:mm:ss} {message}";
        if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(messagePackage);
            Console.ResetColor();
        }
        else
        {
            Trace.WriteLine(messagePackage);
        }
    }

    public void LogError(string message)
    {
        if (_logLevel < LogLevel.Error)
            return;
        var messagePackage = $"[ERROR] {DateTime.Now:HH:mm:ss} {message}";
        if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(messagePackage);
            Console.ResetColor();
        }
        else
        {
            Trace.WriteLine(messagePackage);
        }
    }

    public void LogDebug(string message)
    {
        if (_logLevel < LogLevel.Debug)
            return;
        var messagePackage = $"[DEBUG] {DateTime.Now:HH:mm:ss} {message}";
        if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(messagePackage);
            Console.ResetColor();
        }
        else
        {
            Trace.WriteLine(messagePackage);
        }
    }
}