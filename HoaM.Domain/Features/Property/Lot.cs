using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a piece of land or real estate identified by a unique <see cref="LotId"/>.
    /// </summary>
    public sealed class Lot : Entity<LotId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Lot"/>.
        /// </summary>
        public override LotId Id { get; protected set; } = LotId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Designated <see cref="Lot"/> number as per cadastral.
        /// </summary>
        public LotNumber Number { get; init; } = null!;

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private Lot() { }

        /// <summary>
        /// Creates a new instance of <see cref="Lot"/> with the specified <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The designated lot number.</param>
        /// <returns>A new instance of <see cref="Lot"/>.</returns>
        public static Lot Create(LotNumber number)
        {
            if (number is null) throw new DomainException(DomainErrors.Lot.NumberNullOrEmpty);

            return new() { Number = number };
        }
    }
}
