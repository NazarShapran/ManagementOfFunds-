using Application.Abstraction.ConsoleWrapper;

namespace Application.Implementation.ConsoleWrapper;

public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string message) => Console.WriteLine(message);
    public string ReadLine() => Console.ReadLine() ?? throw new InvalidOperationException();
}