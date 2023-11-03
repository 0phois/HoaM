using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
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
        public CommunityName Name { get; private set; } = null!;

        /// <summary>
        /// Collection of members of the Home Owner's Association
        /// </summary>
        public ICollection<AssociationMember> AssociationMembers { get; private set; } = new HashSet<AssociationMember>();

        /// <summary>
        /// Collection of recurring <see cref="AssociationFee"/>s managed by the Home Owner's Association
        /// </summary>
        public ICollection<AssociationFee> AssociationFees { get; private set; } = new List<AssociationFee>();

        /// <summary>
        /// Collection of all <see cref="Transaction"/>s within the <see cref="Community"/>
        /// </summary>
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

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

        /// <summary>
        /// Collection of <see cref="NotificationTemplate"/>s generated within the <see cref="Community"/>
        /// </summary>
        public ICollection<NotificationTemplate> Notifications { get; private set; } = new List<NotificationTemplate>();

        private Community() { }

        public static Community Create(CommunityName name)
        {
            if (name is null) throw new DomainException(DomainErrors.Community.NameNullOrEmpty);

            return new() { Name = name };
        }

        public void EditName(CommunityName name)
        {
            if (name is null) throw new DomainException(DomainErrors.Community.NameNullOrEmpty);
            
            if (name == Name) return;

            Name = name;
        }
    }
}


//Document
//events
//Financial - Statement | Fee | Receipt | Expense | Payment | Accounts (Name | balance | transactions)
//Announcements/Bulletins
//Submissions
//Forum - https://github.com/enkodellc/blazorboilerplate/tree/master/src/Shared/Modules/BlazorBoilerplate.Theme.MudBlazor.Demo/Pages/Forum