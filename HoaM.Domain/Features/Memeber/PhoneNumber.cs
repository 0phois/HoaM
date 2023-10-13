using HoaM.Domain.Common;
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
        public CountryCallingCode CountryCode { get; set; }

        /// <summary>
        /// 3-digit number representing the telephone service area
        /// </summary>
        public AreaCode AreaCode { get; set; }

        /// <summary>
        /// 3-digit exchange code of the telephone number
        /// </summary>
        public PhonePrefix Prefix { get; set; }

        /// <summary>
        /// Last four digits of a telephone number
        /// </summary>
        public LineNumber Number { get; set; }

        /// <summary>
        /// The <see cref="PhoneType">type</see> of phone
        /// </summary>
        public PhoneType Type { get; set; }
    }
}
