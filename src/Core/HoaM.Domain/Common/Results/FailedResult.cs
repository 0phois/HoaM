namespace HoaM.Domain.Common
{
    /// <summary>
    /// Internal implementation of the <see cref="Result"/> class representing a failed result.
    /// </summary>
    internal class FailedResult : Result
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public override bool IsSuccess => false;

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public override bool IsFailed => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedResult"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message associated with the failed result.</param>
        public FailedResult(string message) { Message = message; }
    }

    /// <summary>
    /// Internal generic implementation of the <see cref="Result{T}"/> class representing a failed result.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    internal class FailedResult<T> : Result<T>
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public override bool IsSuccess => false;

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public override bool IsFailed => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedResult{T}"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message associated with the failed result.</param>
        public FailedResult(string message) : base(default!) { Message = message; }
    }

}
