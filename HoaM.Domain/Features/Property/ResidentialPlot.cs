using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Details of a residential property within the community/neighborhood
    /// </summary>
    public sealed class ResidentialPlot : CommunityPlot
    {
        /// <summary>
        /// <see cref="AssociationMember"/>s residing at the property address
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Residents { get; private set; } = new List<AssociationMember>().AsReadOnly();

        private ResidentialPlot() { }

        public static ResidentialPlot Create(Lot lot, DevelopmentStatus status)
        {
            if (lot is null) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.CommunityPlot.StatusNotDefined);

            return new() { Lot = lot, Status = status };
        }
    }
}
