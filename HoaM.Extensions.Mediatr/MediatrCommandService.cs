using HoaM.Application.Common;
using MediatR;

namespace HoaM.Extensions.Mediatr
{
    public class MediatrCommandService(IMediator mediator) : ICommandService
    {
        private readonly IMediator _mediator = mediator;

        public async Task ExecuteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand
        {
            await _mediator.Send(new MediatrCommand<ICommand>(request), cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default) 
        {
            return (TResponse)await _mediator.Send(new MediatrCommand<ICommand<TResponse>, TResponse>(request), cancellationToken).ConfigureAwait(false);
        }
    }
}
