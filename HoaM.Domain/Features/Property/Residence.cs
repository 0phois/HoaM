using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Details of a residential property within the community/neighborhood
    /// </summary>
    public sealed class Residence : Parcel
    {
        /// <summary>
        /// <see cref="AssociationMember"/>s residing at the property address
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Residents { get; private set; } = new List<AssociationMember>().AsReadOnly();

        private Residence() { }

        public static Residence Create(DevelopmentStatus status, params Lot[] lots)
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            return new Residence() { Status = status }.WithLots<Residence>(lots);
        }
    }
}
