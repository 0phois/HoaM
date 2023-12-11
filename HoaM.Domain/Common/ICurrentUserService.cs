using HoaM.Domain.Features;

namespace HoaM.Domain
{
    public interface ICurrentUserService
    {
        IMember CurrentUser { get; }
    }
}
