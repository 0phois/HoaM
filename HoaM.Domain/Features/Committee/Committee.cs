using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Defines a committee within the Home Owner's Association
    /// </summary>
    public sealed class Committee : Entity<CommitteeId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Committee"/>
        /// </summary>
        public override CommitteeId Id => CommitteeId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Committee"/> (example: Executive)
        /// </summary>
        public CommitteeName Name { get; private set; } = null!;

        /// <summary>
        /// <see cref="Domain.MissionStatement"/> of the <see cref="Committee"/>
        /// </summary>
        public MissionStatement MissionStatement { get; set; } = null!;

        /// <summary>
        /// Other relevant details about the <see cref="Committee"/>
        /// </summary>
        public IReadOnlyCollection<Note> AdditionalDetails => _additionalDetails.AsReadOnly();
        private readonly List<Note> _additionalDetails = new();

        /// <summary>
        /// Date the <see cref="Committee"/> was created
        /// </summary>
        public DateOnly? Established { get; set; }

        /// <summary>
        /// Date the <see cref="Committee"/> ceased operations
        /// </summary>
        public DateOnly? Dissolved { get; private set; }

        /// <summary>
        /// <see cref="AssociationMember"/>s that make up the <seealso cref="Committee"/>
        /// </summary>
        public ICollection<CommitteeMember> Members { get; set; } = new HashSet<CommitteeMember>();

        /// <summary>
        /// All meetings held by this <see cref="Committee"/>
        /// </summary>
        public ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
        public bool IsDeleted => DeletionDate is not null;

        /// <summary>
        /// Whether or not the <see cref="Committee"/> is active 
        /// </summary>
        public bool IsActive => DeletionDate is null && Dissolved is null;

        private Committee() { }

        public static Committee Create(CommitteeName name, DateOnly? established = null)
        {
            return new() { Name = name, Established = established };
        }

        public Committee WithMissionStatement(MissionStatement statement)
        {
            Validate();
            
            MissionStatement = statement;

            return this;
        }

        public Committee WithAdditionalDetails(params Note[] details)
        {
            Validate();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.MissingDetails);

            _additionalDetails.Clear();

            _additionalDetails.AddRange(details);

            return this;
        }

        public Committee AppendAdditionalDetails(params Note[] details)
        {
            Validate();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.MissingDetails);

            _additionalDetails.AddRange(details);

            return this;
        }

        public Committee RemoveDetails()
        {
            Validate();

            _additionalDetails.Clear();

            return this;
        }

        public void EditName(CommitteeName name)
        {
            Validate();

            if (name == Name) return;

            Name = name;
        }

        public bool TryDissolve(ISystemClock systemClock)
        {
            if (DeletionDate.HasValue) return false;
            if (Dissolved.HasValue) return false;

            Dissolved = DateOnly.FromDateTime(systemClock.UtcNow.DateTime);

            AddDomainEvent(new CommitteeDissolvedNotification(this));

            return true;
        }

        private void Validate()
        {
            if (DeletionDate.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDeleted);
            if (Dissolved.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDissolved);
        }
    }
}
