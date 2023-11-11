namespace HoaM.Domain.Common
{
    public class Result : IResult
    {
        public virtual bool IsSuccess => true;
        public virtual bool IsFailed => false;

        public string Message { get; init; } = string.Empty;

        public override string ToString() => $"{GetType().Name} {Message}";

        public static implicit operator Task<IResult>(Result value) => Task.FromResult<IResult>(value);
        public static implicit operator ValueTask<IResult>(Result value) => ValueTask.FromResult<IResult>(value);

    }

    public class Result<T> : Result, IResult<T>
    {
        public T Value { get; set; }

        public Result(T value) { Value = value; }

        public static implicit operator Task<IResult<T>>(Result<T> value) => Task.FromResult<IResult<T>>(value);
        public static implicit operator ValueTask<IResult<T>>(Result<T> value) => ValueTask.FromResult<IResult<T>>(value);
    }
}
