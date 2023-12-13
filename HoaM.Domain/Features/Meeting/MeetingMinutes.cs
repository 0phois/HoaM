using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents the minutes of a meeting, documenting details such as agenda notes, action items, notes, attendees, and publication status.
    /// </summary>
    public sealed class MeetingMinutes : AuditableSoftDeleteEntity<MinutesId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the meeting minutes.
        /// </summary>
        public override MinutesId Id { get; protected set; } = MinutesId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets the read-only collection of agenda notes associated with the meeting minutes.
        /// </summary>
        public IReadOnlyCollection<Note> AgendaNotes => _agendaNotes.AsReadOnly();
        private readonly List<Note> _agendaNotes = [];

        /// <summary>
        /// Gets the read-only collection of action items associated with the meeting minutes.
        /// </summary>
        public IReadOnlyCollection<ActionItem> ActionItems => _actionItems.AsReadOnly();
        private readonly List<ActionItem> _actionItems = [];

        /// <summary>
        /// Gets the read-only collection of general notes associated with the meeting minutes.
        /// </summary>
        public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
        private readonly List<Note> _notes = [];

        /// <summary>
        /// Gets the read-only collection of attendees associated with the meeting minutes.
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Attendees => _attendees.AsReadOnly();
        private readonly List<AssociationMember> _attendees = [];

        /// <summary>
        /// Gets or sets the identifier of the publisher associated with the meeting minutes.
        /// </summary>
        public AssociationMemberId? Publisher { get; private set; }

        /// <summary>
        /// Gets or sets the publication date of the meeting minutes.
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the meeting minutes have been published.
        /// </summary>
        public bool IsPublished => PublishedDate is not null;

        /// <summary>
        /// Gets or sets the meeting associated with the minutes.
        /// </summary>
        public Meeting Meeting { get; private set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingMinutes"/> class.
        /// </summary>
        private MeetingMinutes() { }

        /// <summary>
        /// Creates a new instance of the <see cref="MeetingMinutes"/> class for a given meeting.
        /// </summary>
        /// <param name="meeting">The meeting associated with the minutes.</param>
        /// <returns>The created meeting minutes.</returns>
        internal static MeetingMinutes CreateFor(Meeting meeting)
        {
            if (meeting is null) throw new DomainException(DomainErrors.Meeting.NullOrEmpty);

            return new MeetingMinutes() { Meeting = meeting };
        }

        /// <summary>
        /// Sets the attendees of the meeting minutes, replacing any existing attendees.
        /// </summary>
        /// <param name="attendees">The attendees to set.</param>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes WithAttendees(params AssociationMember[] attendees)
        {
            if (attendees is null || attendees.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Clear();
            _attendees.AddRange(attendees);

            return this;
        }

        /// <summary>
        /// Adds attendees to the existing list of attendees.
        /// </summary>
        /// <param name="attendees">The attendees to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes AddAttendees(params AssociationMember[] attendees)
        {
            if (attendees is null || attendees.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.AddRange(attendees);

            return this;
        }

        /// <summary>
        /// Adds a single attendee to the existing list of attendees.
        /// </summary>
        /// <param name="attendee">The attendee to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes AddAttendee(AssociationMember attendee)
        {
            if (attendee is null) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Add(attendee);

            return this;
        }

        /// <summary>
        /// Removes a specific attendee from the list of attendees.
        /// </summary>
        /// <param name="attendee">The attendee to remove.</param>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes RemoveAttendee(AssociationMember attendee)
        {
            if (attendee is null) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Remove(attendee);

            return this;
        }

        /// <summary>
        /// Removes all attendees from the list.
        /// </summary>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes RemoveAttendees()
        {
            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Clear();

            return this;
        }

        /// <summary>
        /// Sets the agenda notes of the meeting minutes, replacing any existing agenda notes.
        /// </summary>
        /// <param name="agendaNotes">The agenda notes to set.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided agenda notes are null or empty or if the meeting minutes are already published.</exception>
        public MeetingMinutes WithAgendaNotes(List<Note> agendaNotes)
        {
            if (agendaNotes is null || agendaNotes.Count == 0)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _agendaNotes.Clear();
            _agendaNotes.AddRange(agendaNotes);

            return this;
        }

        /// <summary>
        /// Adds a single agenda note to the meeting minutes if it is not already present.
        /// </summary>
        /// <param name="agendaNote">The agenda note to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided agenda note is null, or if the meeting minutes are already published.</exception>
        public MeetingMinutes AddAgendaNote(Note agendaNote)
        {
            if (agendaNote is null)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            // If the agenda note is already present in the meeting's agenda, return without adding it.
            if (!Meeting.Agenda.Any(agenda => agenda.Id == agendaNote.Id))
                return this;

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _agendaNotes.Add(agendaNote);

            return this;
        }

        /// <summary>
        /// Removes a specific agenda note from the meeting minutes.
        /// </summary>
        /// <param name="agendaNote">The agenda note to remove.</param>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes RemoveAgendaNote(Note agendaNote)
        {
            _agendaNotes.Remove(agendaNote);

            return this;
        }

        /// <summary>
        /// Removes all agenda notes from the meeting minutes.
        /// </summary>
        /// <returns>The updated meeting minutes.</returns>
        public MeetingMinutes RemoveAgendaNotes()
        {
            _agendaNotes.Clear();

            return this;
        }

        /// <summary>
        /// Sets the general notes of the meeting minutes, replacing any existing notes.
        /// </summary>
        /// <param name="notes">The general notes to set.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided general notes are null or empty or if the meeting minutes are already published.</exception>
        public MeetingMinutes WithNotes(params Note[] notes)
        {
            if (notes is null || notes.Length == 0)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Clear();
            _notes.AddRange(notes);

            return this;
        }

        /// <summary>
        /// Adds multiple general notes to the meeting minutes.
        /// </summary>
        /// <param name="notes">The general notes to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided general notes are null or empty or if the meeting minutes are already published.</exception>
        public MeetingMinutes AddNotes(params Note[] notes)
        {
            if (notes is null || notes.Length == 0)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.AddRange(notes);

            return this;
        }

        /// <summary>
        /// Adds a single general note to the meeting minutes.
        /// </summary>
        /// <param name="note">The general note to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided general note is null or if the meeting minutes are already published.</exception>
        public MeetingMinutes AddNote(Note note)
        {
            if (note is null)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Add(note);

            return this;
        }

        /// <summary>
        /// Removes a specific general note from the meeting minutes.
        /// </summary>
        /// <param name="note">The general note to remove.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown when the provided general note is null or if the meeting minutes are already published.</exception>
        public MeetingMinutes RemoveNote(Note note)
        {
            if (note is null)
                throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Remove(note);

            return this;
        }

        /// <summary>
        /// Removes all general notes from the meeting minutes.
        /// </summary>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown if the meeting minutes are already published.</exception>
        public MeetingMinutes RemoveNotes()
        {
            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Clear();

            return this;
        }

        /// <summary>
        /// Replaces the existing action items with the specified ones.
        /// </summary>
        /// <param name="items">The new action items.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">
        /// Thrown if the provided action items are null or empty, or if the meeting minutes are already published.
        /// </exception>
        public MeetingMinutes WithActionItems(params ActionItem[] items)
        {
            if (items is null || items.Length == 0)
                throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Clear();
            _actionItems.AddRange(items);

            return this;
        }

        /// <summary>
        /// Adds the specified action items to the existing ones.
        /// </summary>
        /// <param name="items">The action items to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">
        /// Thrown if the provided action items are null or empty, or if the meeting minutes are already published.
        /// </exception>
        public MeetingMinutes AddActionItems(params ActionItem[] items)
        {
            if (items is null || items.Length == 0)
                throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.AddRange(items);

            return this;
        }

        /// <summary>
        /// Adds a single action item to the existing ones.
        /// </summary>
        /// <param name="item">The action item to add.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">
        /// Thrown if the provided action item is null or if the meeting minutes are already published.
        /// </exception>
        public MeetingMinutes AddActionItem(ActionItem item)
        {
            if (item is null)
                throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Add(item);

            return this;
        }

        /// <summary>
        /// Removes a specific action item from the meeting minutes.
        /// </summary>
        /// <param name="actionItem">The action item to remove.</param>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">
        /// Thrown if the action item is null, if the meeting minutes have no action items, or if the meeting minutes are already published.
        /// </exception>
        public MeetingMinutes RemoveActionItem(ActionItem actionItem)
        {
            if (_actionItems.Count == 0)
                throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (actionItem is null)
                throw new DomainException(DomainErrors.ActionItem.ItemNullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Remove(actionItem);

            return this;
        }

        /// <summary>
        /// Removes all action items from the meeting minutes.
        /// </summary>
        /// <returns>The updated meeting minutes.</returns>
        /// <exception cref="DomainException">Thrown if the meeting minutes are already published.</exception>
        public MeetingMinutes RemoveActionItems()
        {
            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Clear();

            return this;
        }

        /// <summary>
        /// Marks a specific action item as completed.
        /// </summary>
        /// <param name="item">The action item to mark as completed.</param>
        /// <exception cref="DomainException">
        /// Thrown if the action item is null, if the meeting minutes have no action items, if the action item is not found, or if the meeting minutes are already published.
        /// </exception>
        internal void CompleteActionItem(ActionItem item)
        {
            if (_actionItems.Count == 0)
                throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (item is null)
                throw new DomainException(DomainErrors.ActionItem.ItemNullOrEmpty);

            var actionItem = _actionItems.Find(x => x.Id.Equals(item.Id)) ?? throw new DomainException(DomainErrors.ActionItem.NotFound);

            if (_actionItems.Remove(actionItem))
            {
                actionItem.Complete();
                _actionItems.Add(actionItem);
            }
        }

        /// <summary>
        /// Publishes the meeting minutes with the specified publisher and publication date.
        /// </summary>
        /// <param name="memberId">The ID of the association member publishing the minutes.</param>
        /// <param name="datePublished">The date when the minutes are published.</param>
        /// <exception cref="DomainException">
        /// Thrown if the association member ID is default, if the meeting minutes are already published, or if the publication date is default.
        /// </exception>
        internal void Publish(AssociationMemberId memberId, DateTimeOffset datePublished)
        {
            if (memberId == default)
                throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            if (IsPublished)
                throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            if (datePublished == default)
                throw new DomainException(DomainErrors.MeetingMinutes.DateNullOrEmpty);

            PublishedDate = datePublished;
            Publisher = memberId;
        }
    }
}
