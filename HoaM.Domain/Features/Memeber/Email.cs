using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class Email : Entity<EmailId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Email"/>
        /// </summary>
        public override EmailId Id => EmailId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Whether or not the email address has been verified
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Email address of an <see cref="AssociationMember"/>
        /// </summary>
        public required EmailAddress Address { get; set; }
    }
}
