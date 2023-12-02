using HoaM.Application.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    public class MediatrCommandService(IMediator mediator) : ICommandService
    {
        private readonly IMediator _mediator = mediator;

        public async Task ExecuteAsync<TCommand>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            await _mediator.Send(new MediatrCommand<TCommand>(request), cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteAsync<TCommand, TResponse>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            return (TResponse)await _mediator.Send(new MediatrCommand<TCommand, TResponse>(request), cancellationToken).ConfigureAwait(false);
        }
    }
}
