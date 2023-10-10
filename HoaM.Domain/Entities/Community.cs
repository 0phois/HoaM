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
        public required CommunityName Name { get; init; }

        /// <summary>
        /// Collection of members of the Home Owner's Association
        /// </summary>
        public ICollection<AssociationMember> AssociationMembers { get; private set; } = new HashSet<AssociationMember>();

        /// <summary>
        /// Collection of <see cref="RecurringExpense"/>s managed by the Home Owner's Association
        /// </summary>
        public ICollection<RecurringExpense> Fees { get; private set; } = new List<RecurringExpense>();

        /// <summary>
        /// Collection of <see cref="Committee"/>s within the Home Owner's Association
        /// </summary>
        public ICollection<Committee> Committees { get; private set; } = new HashSet<Committee>();

        /// <summary>
        /// Collection of properties within the <see cref="Community"/>
        /// </summary>
        public ICollection<Residence> Residences { get; private set; } = new HashSet<Residence>();

        /// <summary>
        /// Collection of <see cref="Article"/>s posted in the <seealso cref="Community"/>
        /// </summary>
        public ICollection<Article> Articles { get; private set; } = new List<Article>();
    }
}


//Document
//events
//Financial - Statement | Fee | Receipt | Expense | Payment | Accounts (Name | balance | transactions)
//Announcements/Bulletins
//Submissions
//Forum - https://github.com/enkodellc/blazorboilerplate/tree/master/src/Shared/Modules/BlazorBoilerplate.Theme.MudBlazor.Demo/Pages/Forum