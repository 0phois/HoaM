namespace HoaM.Domain.Exceptions
{
    public sealed record Error(string Code, string Message);

    public static class DomainErrors
    {
        public static class Article
        {
            public static Error AlreadyPublished => new($"{nameof(Article)}.{nameof(AlreadyPublished)}", "The article has already been published.");
        }

        public static class AssociationFee
        {
            public static Error ExpenseNullOrEmpty => new($"{nameof(AssociationFee)}.{ExpenseNullOrEmpty}", "Expense is required.");
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
            public static Error MandateNotFound => new($"{nameof(Committee)}.{nameof(MandateNotFound)}", "Committee mandate not found.");
            public static Error MissingDetails => new($"{nameof(Committee)}.{nameof(MissingDetails)}", "Additional details cannot be empty.");
            public static Error AlreadyDeleted => new($"{nameof(Committee)}.{nameof(AlreadyDeleted)}", "This committee no longer exists.");
            public static Error AlreadyDissolved => new($"{nameof(Committee)}.{nameof(AlreadyDissolved)}", "This committee has already been dissolved.");
        }

        public static class Community
        {
            public static Error NameNullOrEmpty => new($"{nameof(Community)}.{NameNullOrEmpty}","Name is required");
        }

        public static class Document
        {
            public static Error DataNullOrEmpty => new($"{nameof(Document)}.{nameof(DataNullOrEmpty)}", "Data is required. Content cannot be empty.");
            public static Error TitleNullOrEmpty => new($"{nameof(Document)}.{nameof(TitleNullOrEmpty)}", "Title (file name) is required.");
        }

        public static class Email
        {
            public static Error AddressNullOrEmpty => new($"{nameof(Email)}.{nameof(AddressNullOrEmpty)}", "Email address cannot be null.");
        }

        public static class Lot
        {
            public static Error NullOrEmpty => new($"{nameof(Lot)}.{NullOrEmpty}", "Lot cannot be null.");
        }

        public static class Meeting
        {
            public static Error NullOrEmpty => new($"{nameof(Meeting)}.{NullOrEmpty}", "Meeting cannot be null.");

            public static Error MinutesAlreadyAttached
                => new($"{nameof(Meeting)}.{nameof(MinutesAlreadyAttached)}", "Details can not longer be modified. Meeting minutes have already been attached.");

            public static Error MissingAgenda => new($"{nameof(Meeting)}.{nameof(MissingAgenda)}", "Agenda is required.");

            public static Error MissingNote => new($"{nameof(Meeting)}.{nameof(MissingNote)}", "Note is required.");
        }

        public static class MeetingMinutes
        {
            public static Error AlreadyPublished => new($"{nameof(MeetingMinutes)}.{nameof(AlreadyPublished)}", "The meeting minutes have already been published.");
        }

        public static class Note
        {
            public static Error ContentNullOrEmpty => new($"{nameof(Document)}.{nameof(ContentNullOrEmpty)}", "Content is required.");
        }

        public static class Notification
        {
            public static Error AlreadyPublished => new($"{nameof(Notification)}.{nameof(AlreadyPublished)}", "Notification has already been published.");
        }

        public static class NotificationTemplate
        {
            public static Error NullOrEmpty => new($"{nameof(NotificationTemplate)}.{NullOrEmpty}", "Notification template cannot be null.");
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
            public static Error LastDigitsNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(LastDigitsNullOrEmpty)}", "Phone number last four digits are required.");
            public static Error TypeNotDefined => new($"{nameof(PhoneNumber)}.{nameof(TypeNotDefined)}", "Phone number type is invalid.");
            public static Error DuplicateType => new($"{nameof(PhoneNumber)}.{nameof(DuplicateType)}", "The specified phone number type is already registered.");
        }

        public static class Residence
        {
            public static Error NullOrEmpty => new($"{nameof(Residence)}.{nameof(NullOrEmpty)}", "Residence is required.");
            public static Error StatusNotDefined => new($"{nameof(Residence)}.{nameof(StatusNotDefined)}", "Development status is invalid.");
            public static Error StreetNumberNullOrEmpty => new($"{nameof(Residence)}.{nameof(StreetNumberNullOrEmpty)}", "Street number is required.");
            public static Error StreetNameNullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(StreetNameNullOrEmpty)}", "Street name is required.");

        }
    }
}
