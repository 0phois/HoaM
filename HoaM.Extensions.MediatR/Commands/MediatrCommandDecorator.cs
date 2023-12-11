using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed class MediatrCommand<TCommand>(TCommand command) : IRequest<IResult> where TCommand : ICommand
    {
        public TCommand Command { get; } = command;
    }

    internal sealed class BoundMediatrCommand<TCommand>(TCommand command) : IRequest<IResult> where TCommand : ICommand, IBaseCommandBinder
    {
        public TCommand Command { get; } = command;
    }

    internal sealed class MediatrCommand<TCommand, TResponse>(TCommand command) : IRequest<IResult<TResponse>> where TCommand : ICommand<TResponse>
    {
        public TCommand Command { get; } = command;
    }

    internal sealed class MediatrCommandHandler<TCommand>(ICommandHandler<TCommand> commandHandler)
    : IRequestHandler<MediatrCommand<TCommand>, IResult> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler = commandHandler;

        public async Task<IResult> Handle(MediatrCommand<TCommand> request, CancellationToken cancellationToken)
        {
            return await _commandHandler.Handle(request.Command, cancellationToken);
        }
    }

    internal sealed class MediatrCommandHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> commandHandler)
        : IRequestHandler<MediatrCommand<TCommand, TResponse>, IResult<TResponse>> where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _commandHandler = commandHandler;

        public async Task<IResult<TResponse>> Handle(MediatrCommand<TCommand, TResponse> request, CancellationToken cancellationToken)
        {
            return await _commandHandler.Handle(request.Command, cancellationToken);
        }
    }
}
