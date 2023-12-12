using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    public class MediatrCommandService(IMediator mediator) : ICommandService
    {
        private readonly IMediator _mediator = mediator;

        public Task<IResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : ICommand<IResult>
        {
            return _mediator.Send(new MediatrRequestAdapter<TCommand, IResult>(command), cancellationToken);
        }

        public Task<IResult<TResponse>> ExecuteAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<IResult<TResponse>>
        {
            return _mediator.Send(new MediatrRequestAdapter<TCommand, IResult<TResponse>>(command), cancellationToken);
        }
    }
}
