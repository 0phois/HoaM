using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class PhoneNumber : Entity<PhoneNumberId>
    {
        /// <summary>
        /// Unique ID of the <see cref="PhoneNumber"/>
        /// </summary>
        public override PhoneNumberId Id => PhoneNumberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// International subscriber dialing code (telephone country code)
        /// </summary>
        public CountryCallingCode CountryCode { get; init; }

        /// <summary>
        /// 3-digit number representing the telephone service area
        /// </summary>
        public AreaCode AreaCode { get; init; }

        /// <summary>
        /// 3-digit exchange code of the telephone number
        /// </summary>
        public PhonePrefix Prefix { get; init; }

        /// <summary>
        /// Last four digits of a telephone number
        /// </summary>
        public LineNumber Number { get; init; }

        /// <summary>
        /// The <see cref="PhoneType">type</see> of phone
        /// </summary>
        public PhoneType Type { get; init; }

        private PhoneNumber() { }

        public static PhoneNumber Create(PhoneType type, CountryCallingCode countryCode, AreaCode areaCode, PhonePrefix prefix, LineNumber lastDigits)
        {
            if (prefix == default) throw new DomainException(DomainErrors.PhoneNumber.PrefixNullOrEmpty);

            if (prefix == default) throw new DomainException(DomainErrors.PhoneNumber.LastDigitsNullOrEmpty);

            if (!Enum.IsDefined(typeof(PhoneType), type)) throw new DomainException(DomainErrors.PhoneNumber.TypeNotDefined);

            return new() { Type = type, CountryCode = countryCode, AreaCode = areaCode, Prefix = prefix, Number = lastDigits };
        }
    }
}
