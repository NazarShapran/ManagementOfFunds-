using Application.Abstraction.Loggers;

namespace Application.Implementation.Loggers.LoggerTypes;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void LogInfo(string message)
    {
        LogToFile(message, "INFO");
    }

    public void LogError(Exception ex, string message)
    {
        LogToFile(message, "ERROR");
        LogToFile(ex.ToString(), "ERROR");
    }

    private void LogToFile(string message, string type)
    {
        var content = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{type}] {message}";

        using var writer = File.AppendText(_filePath);
        writer.WriteLine(content);
    }
}