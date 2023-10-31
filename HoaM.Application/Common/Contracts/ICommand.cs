using MediatR;

namespace HoaM.Application.Common
{
    public interface ICommand<out TResponse> : IRequest<TResponse> 
    {
        
    }
}
