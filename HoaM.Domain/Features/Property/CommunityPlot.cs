using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public abstract class CommunityPlot : Entity<PlotId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="CommunityPlot"/>
        /// </summary>
        public override PlotId Id => PlotId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Lot# for the <see cref="CommunityPlot"/>
        /// </summary>
        public Lot Lot { get; protected set; } = null!;

        /// <summary>
        /// Street number for the <see cref="CommunityPlot"/>
        /// </summary>
        public StreetNumber StreetNumber { get; protected set; }

        /// <summary>
        /// Street name for the <see cref="CommunityPlot"/>
        /// </summary>
        public StreetName? StreetName { get; protected set; }

        /// <summary>
        /// Development status of the <see cref="CommunityPlot"/>
        /// </summary>
        public DevelopmentStatus Status { get; protected set; }

        /// <summary>
        /// Summary of all <see cref="ITransaction"/>s linked to this <seealso cref="CommunityPlot"/>
        /// </summary>
        public IReadOnlyCollection<ITransaction> Transactions { get; protected set; } = new List<ITransaction>().AsReadOnly();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        private protected CommunityPlot() { }

        public CommunityPlot WithAddress(StreetNumber houseNumber, StreetName streetName)
        {
            if (houseNumber == default) throw new DomainException(DomainErrors.CommunityPlot.StreetNumberNullOrEmpty);

            if (streetName is null) throw new DomainException(DomainErrors.CommunityPlot.StreetNameNullOrEmpty);

            StreetNumber = houseNumber;
            StreetName = streetName;

            return this;
        }

        public void EditStreetName(StreetName streetName)
        {
            if (streetName is null) throw new DomainException(DomainErrors.CommunityPlot.StreetNameNullOrEmpty);

            if (streetName == StreetName) return;

            StreetName = streetName;
        }

        public void EditStreetNumber(StreetNumber streetNumber)
        {
            if (streetNumber == default) throw new DomainException(DomainErrors.CommunityPlot.StreetNumberNullOrEmpty);

            if (streetNumber == StreetNumber) return;

            StreetNumber = streetNumber;
        }

        public void UpdateDevelopmentStatus(DevelopmentStatus status)
        {
            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.CommunityPlot.StatusNotDefined);

            if (status == Status) return;

            Status = status;
        }
    }
}