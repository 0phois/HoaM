namespace HoaM.Domain.Exceptions
{
    public sealed record Error(string Code, string Message);

    public static class DomainErrors
    {
        public static class ActionItem
        {
            public static Error NullOrEmpty => new($"{nameof(ActionItem)}.{nameof(NullOrEmpty)}", "Action items contains no elements.");
            public static Error ItemNullOrEmpty => new($"{nameof(ActionItem)}.{nameof(ItemNullOrEmpty)}", "Action item cannot be null.");
            public static Error NotFound => new($"{nameof(ActionItem)}.{nameof(NotFound)}", "Action item does not exist.");
        }

        public static class Article
        {
            public static Error TitleNullOrEmpty => new($"{nameof(Article)}.{nameof(TitleNullOrEmpty)}", "Title is required.");
            public static Error BodyNullOrEmpty => new($"{nameof(Article)}.{nameof(BodyNullOrEmpty)}", "Body is required.");
            public static Error DateNullOrEmpty => new($"{nameof(Article)}.{nameof(DateNullOrEmpty)}", "Published date is required.");
            public static Error AlreadyPublished => new($"{nameof(Article)}.{nameof(AlreadyPublished)}", "The article has already been published.");
        }

        public static class AssociationFee
        {
            public static Error ExpenseNullOrEmpty => new($"{nameof(AssociationFee)}.{nameof(ExpenseNullOrEmpty)}", "Expense is required.");
            public static Error DateNullOrEmpty => new($"{nameof(AssociationFee)}.{nameof(DateNullOrEmpty)}", "Effective date is required.");
        }

        public static class AssociationMember
        {
            public static Error NullOrEmpty => new($"{nameof(AssociationMember)}.{nameof(NullOrEmpty)}", "Member cannot be null.");
            public static Error FirstNameNullOrEmpty => new($"{nameof(AssociationMember)}.{nameof(FirstNameNullOrEmpty)}", "First name is required.");
            public static Error LastNameNullOrEmpty => new($"{nameof(AssociationMember)}.{nameof(LastNameNullOrEmpty)}", "Last name is required.");
            public static Error DuplicateEmail => new($"{nameof(AssociationMember)}.{nameof(DuplicateEmail)}", "The specified email is already registered.");
        }

        public static class Committee
        {
            public static Error NameNullOrEmpty => new($"{nameof(Committee)}.{nameof(NameNullOrEmpty)}", "Committee name is required.");
            public static Error DateNullOrEmpty => new($"{nameof(Committee)}.{nameof(DateNullOrEmpty)}", "Date is required.");
            public static Error MissionNullOrEmpty => new($"{nameof(Committee)}.{nameof(MissionNullOrEmpty)}", "Mission statement is required.");
            public static Error DetailsNullOrEmpty => new($"{nameof(Committee)}.{nameof(DetailsNullOrEmpty)}", "Additional details cannot be empty.");
            public static Error AlreadyDeleted => new($"{nameof(Committee)}.{nameof(AlreadyDeleted)}", "This committee no longer exists.");
            public static Error AlreadyDissolved => new($"{nameof(Committee)}.{nameof(AlreadyDissolved)}", "This committee has already been dissolved.");
        }

        public static class CommitteeRole
        {
            public static Error NullOrEmpty => new($"{nameof(CommitteeRole)}.{nameof(NullOrEmpty)}", "Committee role cannot be null.");
        }

        public static class Community
        {
            public static Error NameNullOrEmpty => new($"{nameof(Community)}.{nameof(NameNullOrEmpty)}", "Name is required");
        }

        public static class Parcel
        {
            public static Error NullOrEmpty => new($"{nameof(Parcel)}.{nameof(NullOrEmpty)}", "Residence is required.");
            public static Error StatusNotDefined => new($"{nameof(Parcel)}.{nameof(StatusNotDefined)}", "Development status is invalid.");
            public static Error StreetNumberNullOrEmpty => new($"{nameof(Parcel)}.{nameof(StreetNumberNullOrEmpty)}", "Street number is required.");
            public static Error StreetNameNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(StreetNameNullOrEmpty)}", "Street name is required.");
        }

        public static class Document
        {
            public static Error DataNullOrEmpty => new($"{nameof(Document)}.{nameof(DataNullOrEmpty)}", "Data is required. Content cannot be empty.");
            public static Error TitleNullOrEmpty => new($"{nameof(Document)}.{nameof(TitleNullOrEmpty)}", "Title (file name) is required.");
        }

        public static class Email
        {
            public static Error AddressNullOrEmpty => new($"{nameof(Email)}.{nameof(AddressNullOrEmpty)}", "Email address is required.");
        }

        public static class Event
        {
            public static Error ActivityNullOrEmpty => new($"{nameof(Event)}.{nameof(ActivityNullOrEmpty)}", "Event activity is required.");
            public static Error TitleNullOrEmpty => new($"{nameof(Event)}.{nameof(TitleNullOrEmpty)}", "Event title is required.");
            public static Error OccuranceNullOrEmpty => new($"{nameof(Event)}.{nameof(OccuranceNullOrEmpty)}", "Event occurance is required.");
            public static Error StartNullOrEmpty => new($"{nameof(Event)}.{nameof(StartNullOrEmpty)}", "Event start is required.");
            public static Error StopNullOrEmpty => new($"{nameof(Event)}.{nameof(StopNullOrEmpty)}", "Event stop is required.");
            public static Error InvalidLimit => new($"{nameof(Event)}.{nameof(InvalidLimit)}", "Limit must be greater than zero.");
        }

        public static class Lot
        {
            public static Error NullOrEmpty => new($"{nameof(Lot)}.{nameof(NullOrEmpty)}", "Lot cannot be null.");
            public static Error NumberNullOrEmpty => new($"{nameof(Lot)}.{nameof(NumberNullOrEmpty)}", "Lot number cannot be null.");
            public static Error DuplicateNumber => new($"{nameof(Lot)}.{nameof(DuplicateNumber)}", "Duplicate lot numbers found for this parcel");
        }

        public static class Meeting
        {
            public static Error NullOrEmpty => new($"{nameof(Meeting)}.{nameof(NullOrEmpty)}", "Meeting cannot be null.");
            public static Error HostNullOrEmpty => new($"{nameof(Meeting)}.{nameof(HostNullOrEmpty)}", "Meeting host is required.");
            public static Error NoteNullOrEmpty => new($"{nameof(Meeting)}.{nameof(NoteNullOrEmpty)}", "Note is required.");
            public static Error DateNullOrEmpty => new($"{nameof(Meeting)}.{nameof(DateNullOrEmpty)}", "Scheduled date is required.");
            public static Error TitleNullOrEmpty => new($"{nameof(Meeting)}.{nameof(TitleNullOrEmpty)}", "Meeting title is required.");
            public static Error DescriptionNullOrEmpty => new($"{nameof(Meeting)}.{nameof(DescriptionNullOrEmpty)}", "Meeting description is required.");
            public static Error AgendaNullOrEmpty => new($"{nameof(Meeting)}.{nameof(AgendaNullOrEmpty)}", "Agenda is required.");
            public static Error MinutesMismatch => new($"{nameof(Meeting)}.{nameof(MinutesMismatch)}", "Minutes are associated with another meeting.");
            public static Error MinutesAlreadyAttached
                => new($"{nameof(Meeting)}.{nameof(MinutesAlreadyAttached)}", "Details can not longer be modified. Meeting minutes have already been attached.");
        }

        public static class MeetingManager
        {
            public static Error NullOrEmpty => new($"{nameof(MeetingManager)}.{nameof(NullOrEmpty)}", "Meeting manager cannot be null.");
        }

        public static class MeetingMinutes
        {
            public static Error NullOrEmpty => new($"{nameof(MeetingMinutes)}.{nameof(NullOrEmpty)}", "Meeting minutes cannot be null.");
            public static Error NotFound => new($"{nameof(MeetingMinutes)}.{nameof(NotFound)}", "Meeting minutes not found.");
            public static Error NoteNullOrEmpty => new($"{nameof(MeetingMinutes)}.{nameof(NoteNullOrEmpty)}", "Note is required.");
            public static Error AgendaNullOrEmpty => new($"{nameof(MeetingMinutes)}.{nameof(AgendaNullOrEmpty)}", "Agenda is required.");
            public static Error DateNullOrEmpty => new($"{nameof(MeetingMinutes)}.{nameof(DateNullOrEmpty)}", "Published date is required.");
            public static Error AttendeesNullOrEmpty => new($"{nameof(MeetingMinutes)}.{nameof(AttendeesNullOrEmpty)}", "Meeting attendees required.");
            public static Error AlreadyPublished => new($"{nameof(MeetingMinutes)}.{nameof(AlreadyPublished)}", "The meeting minutes have already been published.");
            public static Error InvalidAgendaItem => new($"{nameof(MeetingMinutes)}.{nameof(InvalidAgendaItem)}", "No matching agenda item found for agenda note.");
        }

        public static class Transaction
        {
            public static Error TitleNullOrEmpty => new($"{nameof(Note)}.{nameof(TitleNullOrEmpty)}", "Title is required.");
            public static Error AmountNullOrEmpty => new($"{nameof(Note)}.{nameof(AmountNullOrEmpty)}", "Amount is required.");
            public static Error MemoNullOrEmpty => new($"{nameof(Note)}.{nameof(MemoNullOrEmpty)}", "Memo is required.");
            public static Error DateNullOrEmpty => new($"{nameof(Note)}.{nameof(DateNullOrEmpty)}", "Date is required.");
        }

        public static class Note
        {
            public static Error ContentNullOrEmpty => new($"{nameof(Note)}.{nameof(ContentNullOrEmpty)}", "Content is required.");
        }

        public static class Notification
        {
            public static Error NullOrEmpty => new($"{nameof(Notification)}.{nameof(NullOrEmpty)}", "Notification cannot be null.");
            public static Error DateNullOrEmpty => new($"{nameof(Notification)}.{nameof(DateNullOrEmpty)}", "Date is required.");
            public static Error NotReceived => new($"{nameof(Notification)}.{nameof(NotReceived)}", "Notification has not been received.");
            public static Error AlreadyPublished => new($"{nameof(Notification)}.{nameof(AlreadyPublished)}", "Notification has already been published.");
        }

        public static class NotificationTemplate
        {
            public static Error NullOrEmpty => new($"{nameof(NotificationTemplate)}.{nameof(NullOrEmpty)}", "Notification template cannot be null.");
            public static Error TitleNullOrEmpty => new($"{nameof(NotificationTemplate)}.{nameof(TitleNullOrEmpty)}", "Title is required.");
            public static Error ContentNullOrEmpty => new($"{nameof(NotificationTemplate)}.{nameof(ContentNullOrEmpty)}", "Content is required.");
            public static Error TypeNotDefined => new($"{nameof(NotificationTemplate)}.{nameof(TypeNotDefined)}", "Notification type is invalid.");

        }

        public static class NotificationManager
        {
            public static Error NullOrEmpty => new($"{nameof(NotificationManager)}.{nameof(NullOrEmpty)}", "Notification manager cannot be null.");
        }

        public static class PhoneNumber
        {
            public static Error NullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(NullOrEmpty)}", "Phone number is required.");
            public static Error PrefixNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(PrefixNullOrEmpty)}", "Phone number prefix is required.");
            public static Error CountryCodeNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(CountryCodeNullOrEmpty)}", "Phone number country code is required.");
            public static Error AreaCodeNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(AreaCodeNullOrEmpty)}", "Phone number area code is required.");
            public static Error LastDigitsNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(LastDigitsNullOrEmpty)}", "Phone number last four digits are required.");
            public static Error TypeNotDefined => new($"{nameof(PhoneNumber)}.{nameof(TypeNotDefined)}", "Phone number type is invalid.");
            public static Error DuplicateType => new($"{nameof(PhoneNumber)}.{nameof(DuplicateType)}", "The specified phone number type is already registered.");
        }

        public static class GreenSpace
        {
            public static Error NullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(NullOrEmpty)}", "Green space is required.");
            public static Error RuleNullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(RuleNullOrEmpty)}", "A rule is required.");
            public static Error AmenityNullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(AmenityNullOrEmpty)}", "An amenity is required.");
            public static Error TitleNullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(TitleNullOrEmpty)}", "Title is required.");
            public static Error HoursNullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(HoursNullOrEmpty)}", "Opening hours required.");
            public static Error DescriptionNullOrEmpty => new($"{nameof(GreenSpace)}.{nameof(DescriptionNullOrEmpty)}", "Description is required.");

        }

        public static class Residence
        {
            public static Error NullOrEmpty => new($"{nameof(Residence)}.{nameof(NullOrEmpty)}", "Residence is required.");
        }

        public static class Schedule
        {
            public static Error NullOrEmpty => new($"{nameof(Schedule)}.{nameof(NullOrEmpty)}", "Schedule is required.");
            public static Error OccuranceNullOrEmpty => new($"{nameof(Schedule)}.{nameof(OccuranceNullOrEmpty)}", "First occurance is required.");
            public static Error IntervalTooSmall => new($"{nameof(Schedule)}.{nameof(IntervalTooSmall)}", "Scheduled interval cannot be less than one (1) day.");
        }
    }
}
