namespace HoaM.Application.Common
{
    public interface ICommandService
    {
        Task ExecuteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand;

        Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);
    }
}
