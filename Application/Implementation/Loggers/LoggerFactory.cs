using Application.Abstraction.ConsoleWrapper;
using Application.Abstraction.Loggers;
using Application.Implementation.Loggers.LoggerTypes;
using Microsoft.Extensions.Configuration;

namespace Application.Implementation.Loggers;

public static class LoggerFactory
{
    private const string LoggerSettingsFilepath = "LoggerSettings:FilePath";
    private const string LoggerSettingsType = "LoggerSettings:Type";
    private const string? ConsoleLoggerType = "Console";
    private const string? FileLoggerType = "File";

    public static ILogger CreateLogger(IConfiguration configuration, IConsoleWrapper consoleWrapper)
    {
        var loggerType = configuration[LoggerSettingsType];
        var filePath = configuration[LoggerSettingsFilepath];

        return loggerType switch
        {
            ConsoleLoggerType => new ConsoleLogger(consoleWrapper),
            FileLoggerType => new FileLogger(filePath ?? "logs/app.log"),
            _ => throw new ArgumentException("Invalid logger type in configuration.")
        };
    }
}