using NLog;

namespace Common.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void Debug(string message)
        {
            logger.Debug(message);
        }
        public void Debug(Exception exception)
        {
            logger.Debug(exception);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
        public void Error(Exception exception)
        {
            logger.Error(exception);
        }
        public void Error(string message, Exception exception, params object[] args)
        {
            logger.Error(message, exception, args);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
        public void Info(Exception exception)
        {
            logger.Info(exception);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }
        public void Warn(Exception exception)
        {
            logger.Warn(exception);
        }
    }
}
