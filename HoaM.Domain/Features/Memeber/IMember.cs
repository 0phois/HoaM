namespace HoaM.Domain.Features
{
    public interface IMember
    {
        AssociationMemberId Id { get; }

        /// <summary>
        /// Username of the <see cref="IMember"/> 
        /// </summary>
        Username DisplayName { get; }
    }
}
