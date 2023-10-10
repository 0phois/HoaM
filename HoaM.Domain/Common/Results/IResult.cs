namespace HoaM.Domain.Common
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailed { get; }

        string? Message { get; }
    }

    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }

    public interface IFailedResult : IResult { }

    public interface IExceptionResult : IFailedResult
    {
        Exception Exception { get; }
    }
}
