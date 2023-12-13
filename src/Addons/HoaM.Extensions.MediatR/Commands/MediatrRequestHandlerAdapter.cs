using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed class MediatrRequestHandlerAdapter<TRequest, TResponse>(ICommandHandler<TRequest, TResponse> commandHandler) : IRequestHandler<MediatrRequestAdapter<TRequest, TResponse>, TResponse>
        where TResponse : IResult
    {
        private readonly ICommandHandler<TRequest, TResponse> _commandHandler = commandHandler;

        public Task<TResponse> Handle(MediatrRequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            return _commandHandler.Handle(request.Value, cancellationToken);
        }
    }
}
