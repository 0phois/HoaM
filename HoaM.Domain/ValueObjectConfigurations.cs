using System.Globalization;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace HoaM.Domain
{
    internal partial class ValueObjectConfigurations : ITypelySpecification
    {
        public void Create(ITypelyBuilder builder)
        {
            var entityId = builder.OfGuid().NotEmpty();

            entityId.For("NotificationTemplateId");
            entityId.For("AssociationMemberId");
            entityId.For("AssociationFeeId");
            entityId.For("NotificationId");
            entityId.For("PhoneNumberId");
            entityId.For("TransactionId");
            entityId.For("CommitteeId");
            entityId.For("CommunityId");
            entityId.For("DocumentId");
            entityId.For("ArticleId");
            entityId.For("MeetingId");
            entityId.For("MinutesId");
            entityId.For("ParcelId");
            entityId.For("AuditId");
            entityId.For("EventId");
            entityId.For("EmailId");
            entityId.For("NoteId");
            entityId.For("LotId");

            var name = builder.OfString().NotEmpty().MaxLength(50).Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

            name.For("CommitteeRole").AsClass();
            name.For("TransactionTitle");
            name.For("CommunityName");
            name.For("CommitteeName");
            name.For("ParcelTitle");
            name.For("StreetName");
            name.For("FirstName");
            name.For("LastName");
            name.For("FileName");

            var title = builder.OfString().NotEmpty().MaxLength(100).Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

            title.For("NotificationTitle");
            title.For("MeetingTitle");
            title.For("ArticleTitle");
            title.For("EventTitle");
            title.For("Username");

            builder.OfString().For("EmailAddress").Matches(new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"));
            builder.OfString().For("MeetingDescription").NotEmpty().MaxLength(250);
            builder.OfString().For("MissionStatement").NotEmpty().MaxLength(300);
            builder.OfString().For("LotNumber").NotEmpty().MaxLength(10);
            builder.OfString().For("Text").NotEmpty();

            builder.OfUShort().For("CountryCallingCode").NotEmpty().LessThanOrEqualTo(999).GreaterThanOrEqualTo(1);
            builder.OfUShort().For("AreaCode").NotEmpty().LessThanOrEqualTo(999).GreaterThanOrEqualTo(100);
            builder.OfUShort().For("PhonePrefix").NotEmpty().LessThan(800).GreaterThanOrEqualTo(200);
            builder.OfUShort().For("LineNumber").NotEmpty().LessThanOrEqualTo(9999).GreaterThan(0);
            builder.OfUShort().For("StreetNumber").NotEmpty();


        }
    }
}
