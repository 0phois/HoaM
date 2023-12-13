using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a community managed by a Home Owner's Association.
    /// Inherits from <see cref="Entity{CommunityId}"/>.
    /// </summary>
    public sealed class Community : Entity<CommunityId>
    {
        /// <summary>
        /// Gets or sets the unique ID of the community.
        /// </summary>
        public override CommunityId Id { get; protected set; } = CommunityId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the name of the community managed by the Home Owner's Association.
        /// </summary>
        public CommunityName Name { get; private set; } = null!;

        /// <summary>
        /// Gets the collection of members of the Home Owner's Association.
        /// </summary>
        public IReadOnlyCollection<AssociationMember> AssociationMembers { get; private set; } = Array.Empty<AssociationMember>();

        /// <summary>
        /// Gets the collection of recurring association fees managed by the Home Owner's Association.
        /// </summary>
        public IReadOnlyCollection<AssociationFee> AssociationFees { get; private set; } = Array.Empty<AssociationFee>();

        /// <summary>
        /// Gets the collection of all transactions within the community.
        /// </summary>
        public IReadOnlyCollection<Transaction> Transactions { get; private set; } = Array.Empty<Transaction>();

        /// <summary>
        /// Gets the collection of committees within the Home Owner's Association.
        /// </summary>
        public IReadOnlyCollection<Committee> Committees { get; private set; } = Array.Empty<Committee>();

        /// <summary>
        /// Gets the collection of meetings held in the community.
        /// </summary>
        public IReadOnlyCollection<Meeting> Meeting { get; private set; } = Array.Empty<Meeting>();

        /// <summary>
        /// Gets the collection of events for the community.
        /// </summary>
        public IReadOnlyCollection<Event> Events { get; private set; } = Array.Empty<Event>();

        /// <summary>
        /// Gets the collection of lots in the community.
        /// </summary>
        public IReadOnlyCollection<Lot> Lots { get; private set; } = Array.Empty<Lot>();

        /// <summary>
        /// Gets the collection of properties within the community.
        /// </summary>
        public IReadOnlyCollection<Parcel> Parcels { get; private set; } = Array.Empty<Parcel>();

        /// <summary>
        /// Gets the collection of articles posted in the community.
        /// </summary>
        public IReadOnlyCollection<Article> Articles { get; private set; } = Array.Empty<Article>();

        /// <summary>
        /// Gets the collection of documents stored for the community.
        /// </summary>
        public IReadOnlyCollection<Document> Documents { get; private set; } = Array.Empty<Document>();

        /// <summary>
        /// Gets the collection of notification templates generated within the community.
        /// </summary>
        public IReadOnlyCollection<NotificationTemplate> Notifications { get; private set; } = Array.Empty<NotificationTemplate>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Community"/> class.
        /// </summary>
        private Community() { }

        /// <summary>
        /// Creates a new community with the specified name.
        /// </summary>
        /// <param name="name">The name of the community.</param>
        /// <returns>The newly created community.</returns>
        public static Community Create(CommunityName name)
        {
            if (name is null) throw new DomainException(DomainErrors.Community.NameNullOrEmpty);

            return new() { Name = name };
        }

        /// <summary>
        /// Edits the name of the community.
        /// </summary>
        /// <param name="name">The new name for the community.</param>
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