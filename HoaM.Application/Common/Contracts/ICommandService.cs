using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface ICommandService
    {
        Task<IResult> ExecuteAsync<TCommand>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand;

        Task<IResult<TResponse>> ExecuteAsync<TCommand, TResponse>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>;
    }
}
