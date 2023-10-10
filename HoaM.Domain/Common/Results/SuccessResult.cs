namespace HoaM.Domain.Common
{
    internal class SuccessResult : Result
    {
        public static SuccessResult Instance { get; } = new();

        public SuccessResult() { }

        public SuccessResult(string message) { Message = message; }
    }

    internal class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data) { }

        public SuccessResult(T data, string message) : base(data) { Message = message; }
    }
}
