using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a land parcel identified by a unique <see cref="ParcelId"/>.
    /// </summary>
    public abstract class Parcel : Entity<ParcelId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Parcel"/>.
        /// </summary>
        public override ParcelId Id { get; protected set; } = ParcelId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets the collection of lots associated with the parcel.
        /// </summary>
        public IReadOnlyCollection<Lot> Lots => _lots.AsReadOnly();
        private readonly List<Lot> _lots = [];

        /// <summary>
        /// Gets or sets the street number associated with the parcel.
        /// </summary>
        public StreetNumber? StreetNumber { get; private protected set; }

        /// <summary>
        /// Gets or sets the street name associated with the parcel.
        /// </summary>
        public StreetName? StreetName { get; private protected set; }

        /// <summary>
        /// Gets or sets the development status of the parcel.
        /// </summary>
        public DevelopmentStatus Status { get; private protected set; }

        /// <summary>
        /// Gets the collection of transactions associated with the parcel.
        /// </summary>
        public IReadOnlyCollection<Transaction> Transactions { get; private protected set; } = new List<Transaction>().AsReadOnly();

        /// <summary>
        /// Gets or sets the ID of the user who deleted the parcel.
        /// </summary>
        public AssociationMemberId? DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the parcel was deleted.
        /// </summary>
        public DateTimeOffset? DeletionDate { get; set; }

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private protected Parcel() { }

        /// <summary>
        /// Adds a <see cref="Lot"/> to the parcel.
        /// </summary>
        /// <typeparam name="T">The type of parcel.</typeparam>
        /// <param name="lot">The lot to add.</param>
        /// <returns>The modified parcel instance.</returns>
        public T AddLot<T>(Lot lot) where T : Parcel
        {
            if (lot is null) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (_lots.Exists(x => x.Number.Equals(lot.Number))) throw new DomainException(DomainErrors.Lot.DuplicateNumber);

            _lots.Add(lot);

            return (T)this;
        }

        /// <summary>
        /// Sets the collection of lots associated with the parcel.
        /// </summary>
        /// <typeparam name="T">The type of parcel.</typeparam>
        /// <param name="lots">The lots to associate with the parcel.</param>
        /// <returns>The modified parcel instance.</returns>
        public T WithLots<T>(params Lot[] lots) where T : Parcel
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (lots.DistinctBy(x => x.Number).Count() != lots.Length) throw new DomainException(DomainErrors.Lot.DuplicateNumber);

            _lots.Clear();
            _lots.AddRange(lots);

            return (T)this;
        }

        /// <summary>
        /// Sets the street address associated with the parcel.
        /// </summary>
        /// <typeparam name="T">The type of parcel.</typeparam>
        /// <param name="houseNumber">The street number.</param>
        /// <param name="streetName">The street name.</param>
        /// <returns>The modified parcel instance.</returns>
        public T WithAddress<T>(StreetNumber houseNumber, StreetName streetName) where T : Parcel
        {
            if (houseNumber is null) throw new DomainException(DomainErrors.Parcel.StreetNumberNullOrEmpty);

            if (streetName is null) throw new DomainException(DomainErrors.Parcel.StreetNameNullOrEmpty);

            StreetNumber = houseNumber;
            StreetName = streetName;

            return (T)this;
        }

        /// <summary>
        /// Edits the street name associated with the parcel.
        /// </summary>
        /// <param name="streetName">The new street name.</param>
        public void EditStreetName(StreetName streetName)
        {
            if (streetName is null) throw new DomainException(DomainErrors.Parcel.StreetNameNullOrEmpty);

            if (streetName == StreetName) return;

            StreetName = streetName;
        }

        /// <summary>
        /// Edits the street number associated with the parcel.
        /// </summary>
        /// <param name="streetNumber">The new street number.</param>
        public void EditStreetNumber(StreetNumber streetNumber)
        {
            if (streetNumber is null) throw new DomainException(DomainErrors.Parcel.StreetNumberNullOrEmpty);

            if (streetNumber == StreetNumber) return;

            StreetNumber = streetNumber;
        }

        /// <summary>
        /// Updates the development status of the parcel.
        /// </summary>
        /// <param name="status">The new development status.</param>
        public void UpdateDevelopmentStatus(DevelopmentStatus status)
        {
            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            if (status == Status) return;

            Status = status;
        }
    }

}