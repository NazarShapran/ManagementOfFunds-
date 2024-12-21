using Application.Abstraction.ConsoleWrapper;
using Application.Abstraction.Loggers;

namespace Application.Implementation.Loggers.LoggerTypes;

public class ConsoleLogger(IConsoleWrapper console) : ILogger
{
    public void LogInfo(string message)
    {
        console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}");
    }

    public void LogError(Exception ex, string message)
    {
        console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}");
        console.WriteLine(ex.ToString());
    }
}