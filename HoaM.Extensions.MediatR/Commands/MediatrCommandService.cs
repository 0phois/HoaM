using HoaM.Application.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    public class MediatrCommandService(IMediator mediator) : ICommandService
    {
        private readonly IMediator _mediator = mediator;

        public async Task ExecuteAsync(ICommand request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new MediatrCommand<ICommand>(request), cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default) 
        {
            return (TResponse)await _mediator.Send(new MediatrCommand<ICommand<TResponse>, TResponse>(request), cancellationToken).ConfigureAwait(false);
        }
    }
}
