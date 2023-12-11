using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface ICommand<TResponse> where TResponse : IResult { }
}