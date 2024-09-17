namespace Postech8SOAT.FastOrder.WebAPI.Logger;

public class ConsoleLogger : CleanArch.UseCase.Logging.ILogger
{
    public void LogDebug(string message)
    {
        Console.WriteLine($"DEBUG: {message}");
    }

    public void LogError(string message, Exception? exception = null)
    {
        Console.WriteLine($"ERROR: {message}");

        if (exception != null)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
    }

    public void LogInfo(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }

    public void LogVerbose(string message)
    {
        Console.WriteLine($"VERBOSE: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"WARNING: {message}");
    }
}
