using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface ICommandService
    {
        Task<IResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<IResult>;

        Task<IResult<TResponse>> ExecuteAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<IResult<TResponse>>;
    }
}
