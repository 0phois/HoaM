﻿using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class MeetingMinutes : AuditableSoftDeleteEntity<MinutesId>
    {
        /// <summary>
        /// Unique ID of the <see cref="MeetingMinutes"/>
        /// </summary>
        public override MinutesId Id { get; protected set; } = MinutesId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Discussions and decisions for <see cref="Meeting"/> agenda items
        /// </summary>
        public IReadOnlyCollection<Note> AgendaNotes => _agendaNotes.AsReadOnly();
        private readonly List<Note> _agendaNotes = [];

        /// <summary>
        /// Tasks identified for follow-up
        /// </summary>
        public IReadOnlyCollection<ActionItem> ActionItems => _actionItems.AsReadOnly();
        private readonly List<ActionItem> _actionItems = [];

        /// <summary>
        /// Additional notes of the meeting 
        /// </summary>
        public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
        private readonly List<Note> _notes = [];

        /// <summary>
        /// <see cref="AssociationMember"/>s that attended the <seealso cref="Entities.Meeting"/>
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Attendees => _attendees.AsReadOnly();
        private readonly List<AssociationMember> _attendees = [];

        /// <summary>
        /// <see cref="CommitteeMember"/> that published the <see cref="MeetingMinutes"/>
        /// </summary>
        public CommitteeMember? Publisher { get; private set; }

        /// <summary>
        /// Date and time the <see cref="MeetingMinutes"/> were published
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        public bool IsPublished => PublishedDate is not null;

        /// <summary>
        /// <see cref="Entities.Meeting"/> for which the <seealso cref="MeetingMinutes"/> were recorded
        /// </summary>
        public Meeting Meeting { get; private set; } = null!;

        private MeetingMinutes() { }

        internal static MeetingMinutes CreateFor(Meeting meeting)
        {
            if (meeting is null) throw new DomainException(DomainErrors.Meeting.NullOrEmpty);

            return new MeetingMinutes() { Meeting = meeting };
        }

        public MeetingMinutes WithAttendees(params AssociationMember[] attendees)
        {
            if (attendees is null || attendees.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Clear();
            _attendees.AddRange(attendees);

            return this;
        }

        public MeetingMinutes AddAttendees(params AssociationMember[] attendees)
        {
            if (attendees is null || attendees.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.AddRange(attendees);

            return this;
        }

        public MeetingMinutes AddAttendee(AssociationMember attendee)
        {
            if (attendee is null) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Add(attendee);

            return this;
        }

        public MeetingMinutes RemoveAttendee(AssociationMember attendee)
        {
            if (attendee is null) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Remove(attendee);

            return this;
        }

        public MeetingMinutes RemoveAttendees()
        {
            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Clear();

            return this;
        }

        public MeetingMinutes WithAgendaNotes(List<Note> agendaNotes)
        {
            if (agendaNotes is null || agendaNotes.Count == 0) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _agendaNotes.Clear();
            _agendaNotes.AddRange(agendaNotes);

            return this;
        }

        public MeetingMinutes AddAgendaNote(Note agendaNote)
        {
            if (agendaNote is null) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (!Meeting.Agenda.Any(agenda => agenda.Id == agendaNote.Id)) return this;

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _agendaNotes.Add(agendaNote);

            return this;
        }

        public MeetingMinutes RemoveAgendaNote(Note agendaNote)
        {
            _agendaNotes.Remove(agendaNote);

            return this;
        }

        public MeetingMinutes RemoveAgendaNotes()
        {
            _agendaNotes.Clear();

            return this;
        }

        public MeetingMinutes WithNotes(params Note[] notes)
        {
            if (notes is null || notes.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Clear();
            _notes.AddRange(notes);

            return this;
        }

        public MeetingMinutes AddNotes(params Note[] notes)
        {
            if (notes is null || notes.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.AddRange(notes);

            return this;
        }

        public MeetingMinutes AddNote(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Add(note);

            return this;
        }

        public MeetingMinutes RemoveNote(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Remove(note);

            return this;
        }

        public MeetingMinutes RemoveNotes()
        {
            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Clear();

            return this;
        }

        public MeetingMinutes WithActionItems(params ActionItem[] items)
        {
            if (items is null || items.Length == 0) throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Clear();
            _actionItems.AddRange(items);

            return this;
        }

        public MeetingMinutes AddActionItems(params ActionItem[] item)
        {
            if (item is null || item.Length == 0) throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.AddRange(item);

            return this;
        }

        public MeetingMinutes AddActionItem(ActionItem item)
        {
            if (item is null) throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Add(item);

            return this;
        }

        public MeetingMinutes RemoveActionItem(ActionItem actionItem)
        {
            if (_actionItems.Count == 0) throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (actionItem is null) throw new DomainException(DomainErrors.ActionItem.ItemNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Remove(actionItem);

            return this;
        }

        public MeetingMinutes RemoveActionItems()
        {
            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _actionItems.Clear();

            return this;
        }

        public void CompleteActionItem(ActionItem item)
        {
            if (_actionItems.Count == 0) throw new DomainException(DomainErrors.ActionItem.NullOrEmpty);

            if (item is null) throw new DomainException(DomainErrors.ActionItem.ItemNullOrEmpty);

            var actionItem = _actionItems.Find(x => x.Id.Equals(item.Id)) ?? throw new DomainException(DomainErrors.ActionItem.NotFound);

            if (_actionItems.Remove(actionItem))
            {
                actionItem.Complete();

                _actionItems.Add(actionItem);
            }
        }

        internal void Publish(CommitteeMember publisher, DateTimeOffset datePublished)
        {
            if (publisher is null) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            if (datePublished == default) throw new DomainException(DomainErrors.MeetingMinutes.DateNullOrEmpty);

            PublishedDate = datePublished;
            Publisher = publisher;
        }
    }
}
