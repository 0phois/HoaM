using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a phone number entity with various components.
    /// </summary>
    public sealed class PhoneNumber : Entity<PhoneNumberId>
    {
        /// <inheritdoc/>
        public override PhoneNumberId Id { get; protected set; } = PhoneNumberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the country calling code associated with the phone number.
        /// </summary>
        public CountryCallingCode CountryCode { get; init; } = null!;

        /// <summary>
        /// Gets or sets the area code associated with the phone number.
        /// </summary>
        public AreaCode AreaCode { get; init; } = null!;

        /// <summary>
        /// Gets or sets the phone number prefix.
        /// </summary>
        public PhonePrefix Prefix { get; init; } = null!;

        /// <summary>
        /// Gets or sets the last digits of the phone number.
        /// </summary>
        public LineNumber Number { get; init; } = null!;

        /// <summary>
        /// Gets or sets the type of the phone number (e.g., mobile, landline).
        /// </summary>
        public PhoneType Type { get; init; }

        /// <summary>
        /// Private constructor for creating an instance of <see cref="PhoneNumber"/>.
        /// </summary>
        private PhoneNumber() { }

        /// <summary>
        /// Creates a new instance of <see cref="PhoneNumber"/> with the specified parameters.
        /// </summary>
        /// <param name="type">The type of the phone number (e.g., mobile, work).</param>
        /// <param name="countryCode">The country calling code associated with the phone number.</param>
        /// <param name="areaCode">The area code associated with the phone number.</param>
        /// <param name="prefix">The phone number prefix.</param>
        /// <param name="lastDigits">The last digits of the phone number.</param>
        /// <returns>The created <see cref="PhoneNumber"/> instance.</returns>
        public static PhoneNumber Create(PhoneType type, CountryCallingCode countryCode, AreaCode areaCode, PhonePrefix prefix, LineNumber lastDigits)
        {
            if (countryCode is null) throw new DomainException(DomainErrors.PhoneNumber.CountryCodeNullOrEmpty);
            if (areaCode is null) throw new DomainException(DomainErrors.PhoneNumber.AreaCodeNullOrEmpty);
            if (prefix is null) throw new DomainException(DomainErrors.PhoneNumber.PrefixNullOrEmpty);
            if (lastDigits is null) throw new DomainException(DomainErrors.PhoneNumber.LastDigitsNullOrEmpty);

            if (!Enum.IsDefined(typeof(PhoneType), type)) throw new DomainException(DomainErrors.PhoneNumber.TypeNotDefined);

            return new() { Type = type, CountryCode = countryCode, AreaCode = areaCode, Prefix = prefix, Number = lastDigits };
        }
    }

}
