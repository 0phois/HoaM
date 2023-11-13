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

        public static ResidentialPlot Create(DevelopmentStatus status, params Lot[] lots)
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.CommunityPlot.StatusNotDefined);

            return new ResidentialPlot() { Status = status }.WithLots<ResidentialPlot>(lots);
        }
    }
}
