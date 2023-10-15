namespace HoaM.Domain.Exceptions
{
    public sealed class DomainException : Exception
    {
        public Error Error { get; }

        internal DomainException(Error error) :base(error.Message) => Error = error;
    }
}
