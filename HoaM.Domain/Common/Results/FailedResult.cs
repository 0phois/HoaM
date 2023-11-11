namespace HoaM.Domain.Common
{
    internal class FailedResult : Result
    {
        public override bool IsSuccess => false;
        public override bool IsFailed => true;

        public FailedResult(string message) { Message = message; }
    }

    internal class FailedResult<T> : Result<T>
    {
        public override bool IsSuccess => false;
        public override bool IsFailed => true;

        public FailedResult(string message) : base(default!) { Message = message; }
    }
}
