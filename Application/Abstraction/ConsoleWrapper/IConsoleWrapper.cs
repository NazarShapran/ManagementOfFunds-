namespace Application.Abstraction.ConsoleWrapper;

public interface IConsoleWrapper
{    
    void WriteLine(string message);
    string ReadLine();
}