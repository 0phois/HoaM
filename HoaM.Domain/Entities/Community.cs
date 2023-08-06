using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public sealed class Community : Entity<CommunityId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Community"/>
        /// </summary>
        public override CommunityId Id => CommunityId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the community the Home Owner's Association manages
        /// </summary>
        public CommunityName Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RecurringExpense> Fees { get; private set; } = new List<RecurringExpense>();

        /// <summary>
        /// List of <see cref="Committee"/>s within the Home Owner's Association
        /// </summary>
        public ICollection<Committee> Committees { get; private set; } = new HashSet<Committee>();

        /// <summary>
        /// List of properties within the <see cref="Community"/>
        /// </summary>
        public ICollection<Residence> Residences { get; private set; } = new HashSet<Residence>();

        /// <summary>
        /// List of members of the Home Owner's Association
        /// </summary>
        public ICollection<AssociationMember> AssociationMembers { get; private set; } = new HashSet<AssociationMember>();
    }
}


//Document
//events
//Financial - Statement | Fee | Receipt | Expense | Payment | Accounts (Name | balance | transactions)
//Announcements/Bulletins
//Submissions
