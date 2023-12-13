using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a committee as an entity with an identifier of type <see cref="CommitteeId"/>.
    /// Implements the <see cref="ISoftDelete"/> interface.
    /// </summary>
    public sealed class Committee : Entity<CommitteeId>, ISoftDelete
    {
        /// <summary>
        /// Gets or sets the unique identifier for the committee.
        /// </summary>
        public override CommitteeId Id { get; protected set; } = CommitteeId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the name of the committee (example: Executive).
        /// </summary>
        public CommitteeName Name { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the mission statement of the committee.
        /// </summary>
        public MissionStatement? MissionStatement { get; private set; }

        /// <summary>
        /// Gets the additional details about the committee.
        /// </summary>
        public IReadOnlyCollection<Note> AdditionalDetails => _additionalDetails.AsReadOnly();
        private readonly List<Note> _additionalDetails = new List<Note>();

        /// <summary>
        /// Gets or sets the date the committee was established.
        /// </summary>
        public DateOnly? EstablishedDate { get; set; }

        /// <summary>
        /// Gets or sets the date the committee ceased operations.
        /// </summary>
        public DateOnly? DissolvedDate { get; private set; }

        /// <summary>
        /// Gets or sets the members that make up the committee.
        /// </summary>
        public ICollection<AssociationMember> Members { get; set; } = new HashSet<AssociationMember>();

        /// <summary>
        /// Gets or sets all meetings held by this committee.
        /// </summary>
        public ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();

        /// <summary>
        /// Gets or sets the identifier of the member who deleted the committee.
        /// </summary>
        public AssociationMemberId? DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the committee was deleted.
        /// </summary>
        public DateTimeOffset? DeletionDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether the committee is deleted.
        /// </summary>
        public bool IsDeleted => DeletionDate is not null;

        /// <summary>
        /// Gets a value indicating whether the committee is dissolved.
        /// </summary>
        public bool IsDissolved => DissolvedDate is not null;

        /// <summary>
        /// Gets a value indicating whether the committee is active.
        /// </summary>
        public bool IsActive => DeletionDate is null && DissolvedDate is null;

        /// <summary>
        /// Private constructor to prevent direct instantiation of the committee without using the creation method.
        /// </summary>
        private Committee() { }

        /// <summary>
        /// Creates a new committee with the specified name and optional established date.
        /// </summary>
        /// <param name="name">The name of the committee.</param>
        /// <param name="established">The date the committee was established (optional).</param>
        /// <returns>A new instance of the <see cref="Committee"/> class.</returns>
        public static Committee Create(CommitteeName name, DateOnly? established = null)
        {
            if (name is null) throw new DomainException(DomainErrors.Committee.NameNullOrEmpty);

            return new Committee { Name = name, EstablishedDate = established };
        }

        /// <summary>
        /// Sets the established date for the committee.
        /// </summary>
        /// <param name="establishedDate">The date the committee was established.</param>
        /// <returns>The updated committee instance.</returns>
        public Committee WithEstablishedDate(DateOnly establishedDate)
        {
            CheckForActiveCommittee();

            if (establishedDate == default) throw new DomainException(DomainErrors.Committee.DateNullOrEmpty);

            EstablishedDate = establishedDate;

            return this;
        }

        /// <summary>
        /// Sets the mission statement for the committee.
        /// </summary>
        /// <param name="statement">The mission statement of the committee.</param>
        /// <returns>The updated committee instance.</returns>
        public Committee WithMissionStatement(MissionStatement statement)
        {
            CheckForActiveCommittee();

            if (statement is null) throw new DomainException(DomainErrors.Committee.MissionNullOrEmpty);

            MissionStatement = statement;

            return this;
        }

        /// <summary>
        /// Sets additional details for the committee.
        /// </summary>
        /// <param name="details">Additional details about the committee.</param>
        /// <returns>The updated committee instance.</returns>
        public Committee WithAdditionalDetails(params Note[] details)
        {
            CheckForActiveCommittee();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.DetailsNullOrEmpty);

            _additionalDetails.Clear();
            _additionalDetails.AddRange(details);

            return this;
        }

        /// <summary>
        /// Appends additional details for the committee.
        /// </summary>
        /// <param name="details">Additional details about the committee.</param>
        /// <returns>The updated committee instance.</returns>
        public Committee AppendAdditionalDetails(params Note[] details)
        {
            CheckForActiveCommittee();

            if (details is null || details.Length == 0) throw new DomainException(DomainErrors.Committee.DetailsNullOrEmpty);

            _additionalDetails.AddRange(details);

            return this;
        }

        /// <summary>
        /// Removes all additional details for the committee.
        /// </summary>
        /// <returns>The updated committee instance.</returns>
        public Committee RemoveDetails()
        {
            CheckForActiveCommittee();

            _additionalDetails.Clear();

            return this;
        }

        /// <summary>
        /// Edits the name of the committee.
        /// </summary>
        /// <param name="name">The new name for the committee.</param>
        public void EditName(CommitteeName name)
        {
            CheckForActiveCommittee();

            if (name is null) throw new DomainException(DomainErrors.Committee.NameNullOrEmpty);

            if (name == Name) return;

            Name = name;
        }

        /// <summary>
        /// Attempts to dissolve the committee.
        /// </summary>
        /// <param name="systemClock">The system clock used to determine the dissolution date.</param>
        /// <returns>True if the committee is successfully dissolved; false otherwise.</returns>
        public bool TryDissolve(TimeProvider systemClock)
        {
            if (DeletionDate.HasValue) return false;
            if (DissolvedDate.HasValue) return false;

            DissolvedDate = DateOnly.FromDateTime(systemClock.GetUtcNow().DateTime);

            AddDomainEvent(new CommitteeDissolvedNotification(this));

            return true;
        }

        /// <summary>
        /// Checks whether the committee is active and throws exceptions if it is not.
        /// </summary>
        private void CheckForActiveCommittee()
        {
            if (DeletionDate.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDeleted);
            if (DissolvedDate.HasValue) throw new DomainException(DomainErrors.Committee.AlreadyDissolved);
        }
    }
}
