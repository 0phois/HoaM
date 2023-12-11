using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    public abstract class Parcel : Entity<ParcelId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Parcel"/>
        /// </summary>
        public override ParcelId Id { get; protected set; } = ParcelId.From(NewId.Next().ToGuid());

        /// <summary>
        /// All <see cref="Lot"/>s that make up the <see cref="Parcel"/>
        /// </summary>
        public IReadOnlyCollection<Lot> Lots => _lots.AsReadOnly();
        private readonly List<Lot> _lots = [];

        /// <summary>
        /// Street number for the <see cref="Parcel"/>
        /// </summary>
        public StreetNumber? StreetNumber { get; private protected set; }

        /// <summary>
        /// Street name for the <see cref="Parcel"/>
        /// </summary>
        public StreetName? StreetName { get; private protected set; }

        /// <summary>
        /// Development status of the <see cref="Parcel"/>
        /// </summary>
        public DevelopmentStatus Status { get; private protected set; }

        /// <summary>
        /// Summary of all <see cref="ITransaction"/>s linked to this <seealso cref="Parcel"/>
        /// </summary>
        public IReadOnlyCollection<Transaction> Transactions { get; private protected set; } = new List<Transaction>().AsReadOnly();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        private protected Parcel() { }

        public T AddLot<T>(Lot lot) where T : Parcel
        {
            if (lot is null) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (_lots.Exists(x => x.Number.Equals(lot.Number))) throw new DomainException(DomainErrors.Lot.DuplicateNumber);

            _lots.Add(lot);

            return (T)this;
        }

        public T WithLots<T>(params Lot[] lots) where T : Parcel
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (lots.DistinctBy(x => x.Number).Count() != lots.Length) throw new DomainException(DomainErrors.Lot.DuplicateNumber);

            _lots.Clear();
            _lots.AddRange(lots);

            return (T)this;
        }

        public T WithAddress<T>(StreetNumber houseNumber, StreetName streetName) where T : Parcel
        {
            if (houseNumber is null) throw new DomainException(DomainErrors.Parcel.StreetNumberNullOrEmpty);

            if (streetName is null) throw new DomainException(DomainErrors.Parcel.StreetNameNullOrEmpty);

            StreetNumber = houseNumber;
            StreetName = streetName;

            return (T)this;
        }

        public void EditStreetName(StreetName streetName)
        {
            if (streetName is null) throw new DomainException(DomainErrors.Parcel.StreetNameNullOrEmpty);

            if (streetName == StreetName) return;

            StreetName = streetName;
        }

        public void EditStreetNumber(StreetNumber streetNumber)
        {
            if (streetNumber is null) throw new DomainException(DomainErrors.Parcel.StreetNumberNullOrEmpty);

            if (streetNumber == StreetNumber) return;

            StreetNumber = streetNumber;
        }

        public void UpdateDevelopmentStatus(DevelopmentStatus status)
        {
            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            if (status == Status) return;

            Status = status;
        }
    }
}