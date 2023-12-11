namespace HoaM.Domain
{
    public interface ICurrentUserService
    {
        IMember CurrentUser { get; }
    }
}
