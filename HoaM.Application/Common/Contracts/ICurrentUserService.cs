using HoaM.Domain.Features;

namespace HoaM.Application.Common
{
    public interface ICurrentUserService
    {
        IMember CurrentUser { get; }
    }
}
