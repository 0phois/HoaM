using System.Runtime.CompilerServices;

namespace HoaM.Domain.Common
{
    /// <summary>
    /// Internal implementation of the <see cref="FailedResult"/> class representing a failed result with an associated exception.
    /// Implements the <see cref="IExceptionResult"/> interface.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ExceptionResult"/> class with the specified message and exception.
    /// </remarks>
    /// <param name="message">The message associated with the failed result.</param>
    /// <param name="exception">The exception associated with the failed result.</param>
    internal class ExceptionResult(string message, Exception exception) : FailedResult(message), IExceptionResult
    {
        /// <summary>
        /// Gets the exception associated with the failed result.
        /// </summary>
        public Exception Exception { get; } = exception;

        /// <summary>
        /// Creates an <see cref="ExceptionResult"/> from an operation, including information about the calling member, file, and line.
        /// </summary>
        /// <param name="message">The message associated with the failed result.</param>
        /// <param name="exception">The exception associated with the failed result.</param>
        /// <param name="memberName">The name of the calling member (automatically populated).</param>
        /// <param name="sourceFilePath">The path to the source file of the calling member (automatically populated).</param>
        /// <param name="sourceLineNumber">The line number in the source file of the calling member (automatically populated).</param>
        /// <returns>An <see cref="ExceptionResult"/> instance.</returns>
        public static ExceptionResult FromOperation(string message, Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new($"An exception occurred in {memberName} [{sourceFilePath} - line {sourceLineNumber}]. Message: {message}", exception);
        }
    }

    /// <summary>
    /// Internal generic implementation of the <see cref="FailedResult{T}"/> class representing a failed result with an associated exception.
    /// Implements the <see cref="IExceptionResult"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ExceptionResult{T}"/> class with the specified message and exception.
    /// </remarks>
    /// <param name="message">The message associated with the failed result.</param>
    /// <param name="exception">The exception associated with the failed result.</param>
    internal class ExceptionResult<T>(string message, Exception exception) : FailedResult<T>(message), IExceptionResult
    {
        /// <summary>
        /// Gets the exception associated with the failed result.
        /// </summary>
        public Exception Exception { get; } = exception;
    }

}
