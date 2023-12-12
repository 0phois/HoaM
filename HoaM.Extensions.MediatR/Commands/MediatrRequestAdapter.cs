using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed class MediatrRequestAdapter<TRequest, TResponse>(TRequest value) : IRequest<TResponse> where TResponse : IResult
    {
        public TRequest Value { get; } = value;
    }
}
