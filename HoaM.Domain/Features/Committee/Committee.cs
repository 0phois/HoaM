using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Defines a committee within the Home Owner's Association
    /// </summary>
    public sealed class Committee : Entity<CommitteeId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Committee"/>
        /// </summary>
        public override CommitteeId Id { get; protected set; } = CommitteeId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Committee"/> (example: Executive)
        /// </summary>
        public CommitteeName Name { get; private set; } = null!;

        /// <summary>
        /// <see cref="Domain.MissionStatement"/> of the <see cref="Committee"/>
        /// </summary>
        public MissionStatement? MissionStatement { get; private set; }

        /// <summary>
        /// Other relevant details about the <see cref="Committee"/>
        /// </summary>
        public IReadOnlyCollection<Note> AdditionalDetails => _additionalDetails.AsReadOnly();
        private readonly List<Note> _additionalDetails = [];

        /// <summary>
        /// Date the <see cref="Committee"/> was created
        /// </summary>
        public DateOnly? EstablishedDate { get; set; }

        /// <summary>
        /// Date the <see cref="Committee"/> ceased operations
        /// </summary>
        public DateOnly? DissolvedDate { get; private set; }

        /// <summary>
        /// <see cref="AssociationMember"/>s that make up the <seealso cref="Committee"/>
        /// </summary>
        public ICollection<AssociationMember> Members { get; set; } = new HashSet<AssociationMember>();

        /// <summary>
        /// All meetings held by this <see cref="Committee"/>
        /// </summary>
        public ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        public bool IsDeleted => DeletionDate is not null;
        public bool IsDissolved => DissolvedDate is not null;

        /// <summary>
        /// Whether or not the <see cref="Committee"/> is active 
        /// </summary>
        public bool IsActive => DeletionDate is null && DissolvedDate is null;

        private Committee() { }

        public static Committee Create(CommitteeName name, DateOnly? established = null)
        {
            if (name is null) throw new DomainException(DomainErrors.Committee.NameNullOrEmpty);

            return new() { Name = name, EstablishedDate = established };
        }

        public Committee WithEstablishedDate(DateOnly establishedDate)
        {
            CheckForActiveCommittee();

            if (establishedDate == default) throw new DomainException(DomainErrors.Committee.DateNullOrEmpty);

            EstablishedDate = establishedDate;

            return this;
        }

        public Committee WithMissionStatement(MissionStatement statement)
        {
            CheckForActiveCommittee();

            if (statement is null) throw new DomainException(DomainErrors.Committee.MissionNullOrEmpty);

            MissionStatement = statement;

            return this;
        }

        public Committee WithAdditionalDetails(params Note[] details)
        {
            CheckForActiveCommittee();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.DetailsNullOrEmpty);

            _additionalDetails.Clear();

            _additionalDetails.AddRange(details);

            return this;
        }

        public Committee AppendAdditionalDetails(params Note[] details)
        {
            CheckForActiveCommittee();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.DetailsNullOrEmpty);

            _additionalDetails.AddRange(details);

            return this;
        }

        public Committee RemoveDetails()
        {
            CheckForActiveCommittee();

            _additionalDetails.Clear();

            return this;
        }

        public void EditName(CommitteeName name)
        {
            CheckForActiveCommittee();

            if (name is null) throw new DomainException(DomainErrors.Committee.NameNullOrEmpty);

            if (name == Name) return;

            Name = name;
        }

        public bool TryDissolve(TimeProvider systemClock)
        {
            if (DeletionDate.HasValue) return false;
            if (DissolvedDate.HasValue) return false;

            DissolvedDate = DateOnly.FromDateTime(systemClock.GetUtcNow().DateTime);

            AddDomainEvent(new CommitteeDissolvedNotification(this));

            return true;
        }

        private void CheckForActiveCommittee()
        {
            if (DeletionDate.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDeleted);
            if (DissolvedDate.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDissolved);
        }
    }
}
