using System.Runtime.CompilerServices;

namespace HoaM.Domain.Common
{
    internal class ExceptionResult : FailedResult, IExceptionResult
    {
        public Exception Exception { get; }

        public ExceptionResult(string message, Exception exception) : base(message) { Exception = exception; }

        public static ExceptionResult FromOperation(string message, Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new($"An exception occurred in {memberName} [{sourceFilePath} - line {sourceLineNumber}]. Message: {message}", exception);
        }
    }

    internal class ExceptionResult<T> : FailedResult<T>, IExceptionResult
    {
        public Exception Exception { get; }

        public ExceptionResult(string message, Exception exception) : base(message) { Exception = exception; }
    }
}
