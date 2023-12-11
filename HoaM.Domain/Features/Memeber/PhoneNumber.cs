using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    public sealed class PhoneNumber : Entity<PhoneNumberId>
    {
        /// <summary>
        /// Unique ID of the <see cref="PhoneNumber"/>
        /// </summary>
        public override PhoneNumberId Id { get; protected set; } = PhoneNumberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// International subscriber dialing code (telephone country code)
        /// </summary>
        public CountryCallingCode CountryCode { get; init; } = null!;

        /// <summary>
        /// 3-digit number representing the telephone service area
        /// </summary>
        public AreaCode AreaCode { get; init; } = null!;

        /// <summary>
        /// 3-digit exchange code of the telephone number
        /// </summary>
        public PhonePrefix Prefix { get; init; } = null!;

        /// <summary>
        /// Last four digits of a telephone number
        /// </summary>
        public LineNumber Number { get; init; } = null!;

        /// <summary>
        /// The <see cref="PhoneType">type</see> of phone
        /// </summary>
        public PhoneType Type { get; init; }

        private PhoneNumber() { }

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
