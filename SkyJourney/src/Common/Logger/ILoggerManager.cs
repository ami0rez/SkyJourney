namespace Common.Logger
{
    public interface ILoggerManager
    {
        void Info(string message);
        void Info(Exception exception);
        void Warn(string message);
        void Warn(Exception exception);
        void Debug(string message);
        void Debug(Exception exception);
        void Error(string message);
        void Error(string message, Exception exception, params object[] args);
        void Error(Exception exception);
    }
}
