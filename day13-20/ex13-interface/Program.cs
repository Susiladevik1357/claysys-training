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
    private void WriteLog(string level, string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}]: {message}");
        Console.ResetColor();
    }

    public void LogInfo(string message)
    {
        WriteLog("INFO", message, ConsoleColor.Green);
    }

    public void LogWarning(string message)
    {
        WriteLog("WARNING", message, ConsoleColor.Yellow);
    }

    public void LogError(string message)
    {
        WriteLog("ERROR", message, ConsoleColor.Red);
    }
}

public class FileLogger : ILogger
{
    private string filePath = "log.txt";

    private void WriteLog(string level, string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}]: {message}{Environment.NewLine}";
        File.AppendAllText(filePath, logEntry);
    }

    public void LogInfo(string message)
    {
        WriteLog("INFO", message);
    }

    public void LogWarning(string message)
    {
        WriteLog("WARNING", message);
    }

    public void LogError(string message)
    {
        WriteLog("ERROR", message);
    }
}

public class App
{
    private readonly ILogger _logger;

    public App(ILogger logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        _logger.LogInfo("Application started successfully.");
        _logger.LogWarning("A low memory warning.");
        _logger.LogError("Application crashed unexpectedly.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose logger type: 1. Console  2. File");
        string? choice = Console.ReadLine();

        ILogger logger = (choice == "1") ? new ConsoleLogger() : new FileLogger();

        App app = new App(logger);
        app.Run();

        Console.WriteLine("Logging completed.");
    }
}
