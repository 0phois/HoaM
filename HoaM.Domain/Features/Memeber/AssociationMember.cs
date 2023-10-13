using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Defines a member of the home owner's association
    /// </summary>
    public class AssociationMember : Entity<AssociationMemberId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="AssociationMember"/>
        /// </summary>
        public override AssociationMemberId Id => AssociationMemberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// First name of the <see cref="AssociationMember"/> 
        /// </summary>
        public FirstName? FirstName { get; set; }

        /// <summary>
        /// Last name of the <see cref="AssociationMember"/>
        /// </summary>
        public LastName? LastName { get; set; }

        /// <summary>
        /// Email details of the <see cref="AssociationMember"/>
        /// </summary>
        public Email? Email { get; set; }

        /// <summary>
        /// Contact numbers for the <see cref="AssociationMember"/>
        /// </summary>
        public List<PhoneNumber> PhoneNumbers { get; } = new List<PhoneNumber>();

        /// <summary>
        /// Resendential address (within the community) of the <see cref="AssociationMember"/>
        /// </summary>
        public Residence? Residence { get; private set; }

        /// <summary>
        /// <see cref="Notification"/>s delivered to this <see cref="AssociationMember"/>
        /// </summary>
        public List<Notification> Notifications { get; } = new List<Notification>();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}
