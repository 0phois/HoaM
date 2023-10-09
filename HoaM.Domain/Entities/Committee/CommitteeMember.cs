namespace HoaM.Domain.Entities
{
    public sealed class CommitteeMember : AssociationMember
    {
        /// <summary>
        /// <see cref="CommitteeMember"/>'s role in the <seealso cref="Committee"/>
        /// </summary>
        public CommitteeRole Position { get; set; } = null!;

        /// <summary>
        /// <see cref="Committee"/> to which this <seealso cref="CommitteeMember"/> belongs
        /// </summary>
        public ICollection<Committee> Committees { get; private set; } = new HashSet<Committee>();
    }
}
