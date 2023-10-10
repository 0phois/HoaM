using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    /// <summary>
    /// Details of a residential property within the community/neighborhood
    /// </summary>
    public sealed class Residence : Entity<ResidenceId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Residence"/>
        /// </summary>
        public override ResidenceId Id => ResidenceId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Lot# for the <see cref="Residence"/>
        /// </summary>
        public required Lot Lot { get; set; }

        /// <summary>
        /// Street number for the <see cref="Residence"/>
        /// </summary>
        public StreetNumber StreetNumber { get; set; }

        /// <summary>
        /// Street name for the <see cref="Residence"/>
        /// </summary>
        public required StreetName StreetName { get; set; }

        /// <summary>
        /// Development status of the <see cref="Residence"/>
        /// </summary>
        public DevelopmentStatus Status { get; set; }

        /// <summary>
        /// Summary of all <see cref="ITransaction"/>s linked to this <seealso cref="Residence"/>
        /// </summary>
        public List<ITransaction> Transactions { get; private set; } = new List<ITransaction>();

        /// <summary>
        /// <see cref="AssociationMember"/>s residing at the property address
        /// </summary>
        public ICollection<AssociationMember> Residents { get; private set; } = new List<AssociationMember>();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}
