namespace HoaM.Domain.Common
{
    /// <summary>
    /// Static class providing factory methods for creating result instances.
    /// </summary>
    public static class Results
    {
        /// <summary>
        /// Creates a success result without a message.
        /// </summary>
        public static Result Success() => SuccessResult.Instance;

        /// <summary>
        /// Creates a success result with a specified message.
        /// </summary>
        /// <param name="message">The message associated with the success result.</param>
        public static Result Success(string message) => new SuccessResult(message);

        /// <summary>
        /// Creates a success result with a specified data value.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="data">The value associated with the success result.</param>
        public static Result<T> Success<T>(T data) => new SuccessResult<T>(data);

        /// <summary>
        /// Creates a success result with a specified data value and message.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="data">The value associated with the success result.</param>
        /// <param name="message">The message associated with the success result.</param>
        public static Result<T> Success<T>(T data, string message) => new SuccessResult<T>(data, message);

        /// <summary>
        /// Creates a failed result with a specified message.
        /// </summary>
        /// <param name="message">The message associated with the failed result.</param>
        public static Result Failed(string message) => new FailedResult(message);

        /// <summary>
        /// Creates a failed result with a specified message.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="message">The message associated with the failed result.</param>
        public static Result<T> Failed<T>(string message) => new FailedResult<T>(message);

        /// <summary>
        /// Creates an exception result with a specified message and exception.
        /// </summary>
        /// <param name="message">The message associated with the exception result.</param>
        /// <param name="exception">The exception associated with the exception result.</param>
        public static Result Exception(string message, Exception exception) => new ExceptionResult(message, exception);

        /// <summary>
        /// Creates an exception result with a specified message and exception.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="message">The message associated with the exception result.</param>
        /// <param name="exception">The exception associated with the exception result.</param>
        public static Result<T> Exception<T>(string message, Exception exception) => new ExceptionResult<T>(message, exception);
    }

}
