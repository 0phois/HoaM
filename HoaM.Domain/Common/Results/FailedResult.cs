namespace HoaM.Domain.Common
{
    internal class FailedResult : Result
    {
        public FailedResult(string message) { Message = message; }
    }

    internal class FailedResult<T> : Result<T>
    {
        public FailedResult(string message) : base(default!) { Message = message; }
    }
}
