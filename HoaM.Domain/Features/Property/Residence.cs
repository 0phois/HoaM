using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a residential property within the <see cref="Community"/> class.
    /// </summary>
    public sealed class Residence : Parcel
    {
        /// <summary>
        /// Gets the collection of association members residing in the residence.
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Residents { get; private set; } = new List<AssociationMember>().AsReadOnly();

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private Residence() { }

        /// <summary>
        /// Creates a new <see cref="Residence"/> with the specified development status and lots.
        /// </summary>
        /// <param name="status">The development status of the residence.</param>
        /// <param name="lots">The lots associated with the residence.</param>
        /// <returns>The created <see cref="Residence"/> instance.</returns>
        public static Residence Create(DevelopmentStatus status, params Lot[] lots)
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            return new Residence() { Status = status }.WithLots<Residence>(lots);
        }
    }

}
