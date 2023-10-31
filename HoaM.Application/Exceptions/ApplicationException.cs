using HoaM.Domain.Exceptions;

namespace HoaM.Application.Exceptions
{
    public sealed class ApplicationException : Exception
    {
        public Error Error { get; }

        internal ApplicationException(Error error) : base(error.Message) => Error = error;
    }
}
