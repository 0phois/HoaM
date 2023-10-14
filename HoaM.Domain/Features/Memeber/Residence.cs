using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
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
        public Lot Lot { get; private set; } = null!;

        /// <summary>
        /// Street number for the <see cref="Residence"/>
        /// </summary>
        public StreetNumber HouseNumber { get; private set; }

        /// <summary>
        /// Street name for the <see cref="Residence"/>
        /// </summary>
        public StreetName? StreetName { get; private set; }

        /// <summary>
        /// Development status of the <see cref="Residence"/>
        /// </summary>
        public DevelopmentStatus Status { get; set; }

        /// <summary>
        /// Summary of all <see cref="ITransaction"/>s linked to this <seealso cref="Residence"/>
        /// </summary>
        public IReadOnlyCollection<ITransaction> Transactions { get; private set; } = new List<ITransaction>().AsReadOnly();

        /// <summary>
        /// <see cref="AssociationMember"/>s residing at the property address
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Residents { get; private set; } = new List<AssociationMember>().AsReadOnly();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        private Residence() { }

        public static Residence Create(Lot lot, DevelopmentStatus status)
        {
            return new() { Lot = lot, Status = status };
        }

        public Residence WithAddress(StreetNumber houseNumber, StreetName streetName)
        {
            HouseNumber = houseNumber;
            StreetName = streetName; 
            
            return this;
        }

        public void EditStreetName(StreetName streetName) 
        {
            if (streetName == StreetName) return;
            
            StreetName = streetName;
        }

        public void EditHouseNumber(StreetNumber houseNumber)
        {
            if (houseNumber == HouseNumber) return;
            
            HouseNumber = houseNumber;
        }

        public void UpdateDevelopmentStatus(DevelopmentStatus status) 
        {
            if (status == Status) return;

            Status = status;
        }
    }
}
