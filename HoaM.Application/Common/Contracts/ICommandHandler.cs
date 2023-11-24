using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<IResult> Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse> 
    {
        Task<IResult<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
