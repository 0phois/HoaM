namespace HoaM.Application.Common
{
    public interface ICommandService
    {
        Task ExecuteAsync<TCommand>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand;

        Task<TResponse> ExecuteAsync<TCommand, TResponse>(TCommand request, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>;
    }
}
