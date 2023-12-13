namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing the result of an operation.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        bool IsFailed { get; }

        /// <summary>
        /// Gets an optional message associated with the result.
        /// </summary>
        string? Message { get; }
    }

    /// <summary>
    /// Interface representing the result of an operation with a specific value.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Gets the value associated with the result.
        /// </summary>
        T Value { get; }
    }

    /// <summary>
    /// Interface representing a failed result of an operation.
    /// </summary>
    public interface IFailedResult : IResult { }

    /// <summary>
    /// Interface representing a failed result of an operation with an associated exception.
    /// </summary>
    public interface IExceptionResult : IFailedResult
    {
        /// <summary>
        /// Gets the exception associated with the failed result.
        /// </summary>
        Exception Exception { get; }
    }

}
