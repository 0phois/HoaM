using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Defines a committee within the Home Owner's Association
    /// </summary>
    public sealed class Committee : Entity<CommitteeId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Committee"/>
        /// </summary>
        public override CommitteeId Id => CommitteeId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Committee"/> (example: Executive)
        /// </summary>
        public CommitteeName Name { get; private set; } = null!;

        /// <summary>
        /// Date the <see cref="Committee"/> was created
        /// </summary>
        public DateOnly? Established { get; set; }

        /// <summary>
        /// Date the <see cref="Committee"/> ceased operations
        /// </summary>
        public DateOnly? Disolved { get; set; }

        /// <summary>
        /// <see cref="AssociationMember"/>s that make up the <seealso cref="Committee"/>
        /// </summary>
        public ICollection<CommitteeMember> Members { get; set; } = new HashSet<CommitteeMember>();

        /// <summary>
        /// All meetings held by this <see cref="Committee"/>
        /// </summary>
        public ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        private Committee() { }

        public static Committee Create(CommitteeName name, DateOnly? established = null)
        {
            return new() { Name = name, Established = established };
        }

        public void EditName(CommitteeName name)
        {
            if (name == Name) return;

            Name = name;
        }
    }
}
