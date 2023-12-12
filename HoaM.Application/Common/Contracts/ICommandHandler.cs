using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface ICommandHandler<in TCommand, TResponse> where TResponse : IResult
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
