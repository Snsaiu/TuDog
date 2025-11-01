using DryIoc;
using TuDog.Enums;
using TuDog.Interfaces;
using TuDog.Interfaces.Loggers;

namespace TuDog.Extensions;

public static class LoggerExtension
{
    public static void  RegisterConsoleLogger(this IContainer services,LogLevel logLevel)
    {
        services.RegisterInstance(typeof(ILogger),new ConsoleLogger(logLevel));
    }
}