using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    /// <summary>
    /// Defines a member of the home owner's association
    /// </summary>
    public class AssociationMember : Entity<AssociationMemberId>
    {
        /// <summary>
        /// Unique ID of the <see cref="AssociationMember"/>
        /// </summary>
        public override AssociationMemberId Id => AssociationMemberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// First name of the <see cref="AssociationMember"/> 
        /// </summary>
        public FirstName FirstName { get; set; }

        /// <summary>
        /// Last name of the <see cref="AssociationMember"/>
        /// </summary>
        public LastName LastName { get; set; }

        /// <summary>
        /// Email details of the <see cref="AssociationMember"/>
        /// </summary>
        public Email Email { get; set; } = null!;

        /// <summary>
        /// Contact numbers for the <see cref="AssociationMember"/>
        /// </summary>
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        /// <summary>
        /// Resendential address (within the community) of the <see cref="AssociationMember"/>
        /// </summary>
        public Residence? Residence { get; private set; }
    }
}
