using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    public class MediatrCommandService(IMediator mediator) : ICommandService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<IResult> ExecuteAsync<TCommand>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            if (request is IBaseCommandBinder commandBinder)
                return await _mediator.Send(new BoundMediatrCommand<TCommand>(commandBinder), cancellationToken).ConfigureAwait(false);
            else
                return await _mediator.Send(new MediatrCommand<TCommand>(request), cancellationToken).ConfigureAwait(false);
        }

        public async Task<IResult<TResponse>> ExecuteAsync<TCommand, TResponse>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            return await _mediator.Send(new MediatrCommand<TCommand, TResponse>(request), cancellationToken).ConfigureAwait(false);
        }
    }
}
