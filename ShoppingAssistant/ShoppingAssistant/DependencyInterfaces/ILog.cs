namespace ShoppingAssistant.Logging
{
    /// <summary>
    /// Interface for the Log dependencies
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Method that logs an error level message with the given tag and message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        void Error(string tag, string message);

        /// <summary>
        /// Method that logs a warning level message with the given tag and message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        void Warning(string tag, string message);

        /// <summary>
        /// Method that logs a debug level message with the given tag and message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        void Debug(string tag, string message);

        /// <summary>
        /// Method that logs an info level message with the given tag and message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        void Info(string tag, string message);
    }
}
