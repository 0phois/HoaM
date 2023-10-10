namespace HoaM.Domain.Common
{
    internal class ExceptionResult : FailedResult, IExceptionResult
    {
        public Exception Exception { get; }

        public ExceptionResult(string message, Exception exception) : base(message) { Exception = exception; }

        public static ExceptionResult FromOperation(string operation, Exception exception)
        {
            return new($"An exception occurred in {operation}.", exception);
        }
    }

    internal class ExceptionResult<T> : FailedResult<T>, IExceptionResult
    {
        public Exception Exception { get; }

        public ExceptionResult(string message, Exception exception) : base(message) { Exception = exception; }
    }
}
