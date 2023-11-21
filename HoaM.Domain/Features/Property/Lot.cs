using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class Lot : Entity<LotId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Lot"/>
        /// </summary>
        public override LotId Id { get; protected set; } = LotId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Designated <see cref="Lot"/> number as per cadastral
        /// </summary>
        public LotNumber Number { get; init; } = null!;

        private Lot() { }

        public static Lot Create(LotNumber number)
        {
            if (number == null) throw new DomainException(DomainErrors.Lot.NumberNullOrEmpty);
            return new() { Number = number };
        }
    }
}
