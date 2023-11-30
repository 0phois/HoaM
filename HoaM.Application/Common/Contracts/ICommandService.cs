namespace HoaM.Application.Common
{
    public interface ICommandService
    {
        Task ExecuteAsync(ICommand request, CancellationToken cancellationToken = default);

        Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);
    }
}
