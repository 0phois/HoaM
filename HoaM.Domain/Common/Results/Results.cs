namespace HoaM.Domain.Common
{
    public static class Results
    {
        public static Result Success() => SuccessResult.Instance;
        public static Result Success(string message) => new SuccessResult(message);

        public static Result Success<T>(T data) => new SuccessResult<T>(data);
        public static Result Success<T>(T data, string message) => new SuccessResult<T>(data, message);

        public static Result Failed(string message) => new FailedResult(message);
        public static Result Failed<T>(string message) => new FailedResult<T>(message);

        public static Result Exception(string message, Exception exception) => new ExceptionResult(message, exception);
        public static Result Exception<T>(string message, Exception exception) => new ExceptionResult<T>(message, exception);
    }
}
