using System.Globalization;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace HoaM.Domain
{
    internal class ValueObjectConfigurations : ITypelyConfiguration
    {
        public void Configure(ITypelyBuilder builder)
        {
            var entityId = builder.OfGuid().NotEmpty();

            entityId.For("AssociationMemberId");
            entityId.For("AssociationFeeId");
            entityId.For("PhoneNumberId");
            entityId.For("TransactionId");
            entityId.For("ResidenceId");
            entityId.For("CommitteeId");
            entityId.For("CommunityId");
            entityId.For("MeetingId");
            entityId.For("MinutesId");
            entityId.For("EmailId");
            entityId.For("NoteId");

            var name = builder.OfString()
                .NotEmpty()
                .MaxLength(50)
                .Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

            name.For("CommitteeRole").AsClass();
            name.For("TransactionTitle") ;
            name.For("CommunityName");
            name.For("CommitteeName");
            name.For("StreetName");
            name.For("FirstName");
            name.For("LastName");

            builder.OfString().For("MeetingTitle").NotEmpty().MaxLength(150).Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));
            builder.OfString().For("EmailAddress").Matches(new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"));
            builder.OfString().For("MeetingDescription").NotEmpty().MaxLength(250);
            builder.OfString().For("Text").NotEmpty();

            builder.OfUShort().For("CountryCallingCode").NotEmpty().LessThanOrEqualTo(999).GreaterThanOrEqualTo(1);
            builder.OfUShort().For("AreaCode").NotEmpty().LessThanOrEqualTo(999).GreaterThanOrEqualTo(100);
            builder.OfUShort().For("PhonePrefix").NotEmpty().LessThan(800).GreaterThanOrEqualTo(200);
            builder.OfUShort().For("LineNumber").NotEmpty().LessThanOrEqualTo(9999).GreaterThan(0);
            builder.OfUShort().For("StreetNumber").NotEmpty();

            
        }
    }
}
