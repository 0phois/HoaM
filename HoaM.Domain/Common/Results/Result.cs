namespace HoaM.Domain.Common
{
    /// <summary>
    /// Default implementation of the <see cref="IResult"/> interface representing a successful result.
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public virtual bool IsSuccess => true;

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public virtual bool IsFailed => false;

        /// <summary>
        /// Gets or sets an optional message associated with the result.
        /// </summary>
        public string Message { get; init; } = string.Empty;

        /// <summary>
        /// Converts the result to a string representation.
        /// </summary>
        /// <returns>A string representation of the result.</returns>
        public override string ToString() => $"{GetType().Name} {Message}";

        /// <summary>
        /// Implicitly converts a <see cref="Result"/> to a <see cref="Task{IResult}"/>.
        /// </summary>
        public static implicit operator Task<IResult>(Result value) => Task.FromResult<IResult>(value);

        /// <summary>
        /// Implicitly converts a <see cref="Result"/> to a <see cref="ValueTask{IResult}"/>.
        /// </summary>
        public static implicit operator ValueTask<IResult>(Result value) => ValueTask.FromResult<IResult>(value);
    }

    /// <summary>
    /// Generic implementation of the <see cref="IResult{T}"/> interface representing a successful result with a value.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value.
    /// </remarks>
    /// <param name="value">The value associated with the result.</param>
    public class Result<T>(T value) : Result, IResult<T>
    {
        /// <summary>
        /// Gets the value associated with the result.
        /// </summary>
        public T Value { get; } = value;

        /// <summary>
        /// Implicitly converts a <see cref="Result{T}"/> to a <see cref="Task{IResult{T}}"/>.
        /// </summary>
        public static implicit operator Task<IResult<T>>(Result<T> value) => Task.FromResult<IResult<T>>(value);

        /// <summary>
        /// Implicitly converts a <see cref="Result{T}"/> to a <see cref="ValueTask{IResult{T}}"/>.
        /// </summary>
        public static implicit operator ValueTask<IResult<T>>(Result<T> value) => ValueTask.FromResult<IResult<T>>(value);
    }

}
