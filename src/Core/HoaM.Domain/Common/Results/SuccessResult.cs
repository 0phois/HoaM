namespace HoaM.Domain.Common
{
    /// <summary>
    /// Internal implementation of the <see cref="Result"/> class representing a success result without a value.
    /// </summary>
    internal class SuccessResult : Result
    {
        /// <summary>
        /// Gets a singleton instance of the <see cref="SuccessResult"/> class.
        /// </summary>
        public static SuccessResult Instance { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult"/> class.
        /// </summary>
        public SuccessResult() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message associated with the success result.</param>
        public SuccessResult(string message) { Message = message; }
    }

    /// <summary>
    /// Internal generic implementation of the <see cref="Result{T}"/> class representing a success result with a value.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    internal class SuccessResult<T> : Result<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult{T}"/> class with the specified data value.
        /// </summary>
        /// <param name="data">The value associated with the success result.</param>
        public SuccessResult(T data) : base(data) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult{T}"/> class with the specified data value and message.
        /// </summary>
        /// <param name="data">The value associated with the success result.</param>
        /// <param name="message">The message associated with the success result.</param>
        public SuccessResult(T data, string message) : base(data) { Message = message; }
    }

}
