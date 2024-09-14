namespace CleanArch.UseCase.Logging;

public interface ILogger
{
    void LogInfo(string message);
    void LogVerbose(string message);
    void LogWarning(string message);
    void LogDebug(string message);
    void LogError(string message, Exception? exception = null);
}
