using System;
using System.IO;
public interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}
public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[INFO]; " + message);
        Console.ResetColor();
    }
    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[Warning]: " + message);
        Console.ResetColor();
    }
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ERROR]: " + message);
        Console.ResetColor();
    }
}
public class FileLogger : ILogger
{
    private string filePath = "log.txt";
    public void LogInfo(string message)
    {
        File.AppendAllText(filePath, "[INFO]: " + message + Environment.NewLine);
    }
    public void LogWarning(string message)
    {
        File.AppendAllText(filePath, "[WARNING]: " + message + Environment.NewLine);
    }
    public void LogError(string message)
    {
        File.AppendAllText(filePath, "[ERROR]: " + message + Environment.NewLine);
    }
}
public class Application
{
    private readonly ILogger _logger;
        public Application(ILogger logger)
    {
        _logger = logger;
        
    }
    public void Run()
    {
        _logger.LogInfo("Application started Successfully.");
        _logger.LogWarning("ALow memory warning.");
        _logger.LogError("Application crashed unexpectedly.");
    }
}
class Program
{
    static void Main(string[] args)
    {
    
        Console.WriteLine("Choose logger type: 1.Console 2.File");
        string? choice = Console.ReadLine();
        ILogger logger=(choice == "1")? new ConsoleLogger(): new FileLogger();
        Application app = new Application(logger);
        app.Run();
        Console.WriteLine("Logging completed.");
    }
}