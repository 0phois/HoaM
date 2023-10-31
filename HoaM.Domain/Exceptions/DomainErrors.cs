namespace HoaM.Domain.Exceptions
{
    public sealed record Error(string Code, string Message);

    public static class DomainErrors
    {
        public static class Article
        {
            public static Error AlreadyPublished => new($"{nameof(Article)}.{nameof(AlreadyPublished)}", "The article has already been published.");
        }

        public static class AssociationMember
        {
            public static Error DuplicateEmail => new($"{nameof(AssociationMember)}.{nameof(DuplicateEmail)}", "The specified email is already registered.");

            public static Error DuplicateResidenceAssignment
                => new($"{nameof(AssociationMember)}.{nameof(DuplicateResidenceAssignment)}", "This member is already assigned to a different residence.");
        }

        public static class Committee
        {
            public static Error MandateNotFound => new($"{nameof(Committee)}.{nameof(MandateNotFound)}", "Committee mandate not found.");
            public static Error MissingDetails => new($"{nameof(Committee)}.{nameof(MissingDetails)}", "Additional details cannot be empty.");
            public static Error AlreadyDeleted => new($"{nameof(Committee)}.{nameof(AlreadyDeleted)}", "This committee no longer exists.");
            public static Error AlreadyDissolved => new($"{nameof(Committee)}.{nameof(AlreadyDissolved)}", "This committee has already been dissolved.");
        }

        public static class Document
        {
            public static Error NullOrEmpty => new($"{nameof(Document)}.{nameof(NullOrEmpty)}", "Data is required. Content cannot be empty.");
        }

        public static class PhoneNumber
        {
            public static Error NullOrEmpty => new($"{nameof(PhoneNumber)}.{nameof(NullOrEmpty)}", "Phone number is required.");

            public static Error DuplicateType => new($"{nameof(PhoneNumber)}.{nameof(DuplicateType)}", "The specified phone number type is already registered.");
        }

        public static class Meeting
        {
            public static Error MinutesAlreadyAttached
                => new($"{nameof(Meeting)}.{nameof(MinutesAlreadyAttached)}", "Details can not longer be modified. Meeting minutes have already been attached.");

            public static Error MissingAgenda => new($"{nameof(Meeting)}.{nameof(MissingAgenda)}", "Agenda is required.");

            public static Error MissingNote => new($"{nameof(Meeting)}.{nameof(MissingNote)}", "Note is required.");
        }

        public static class MeetingMinutes
        {
            public static Error AlreadyPublished => new($"{nameof(MeetingMinutes)}.{nameof(AlreadyPublished)}", "The meeting minutes have already been published.");
        }

        public static class Notification
        {
            public static Error AlreadyPublished => new($"{nameof(Notification)}.{nameof(AlreadyPublished)}", "Notification has already been published.");
        }

        public static class NotificationManager
        {
            public static Error NullOrEmpty => new($"{nameof(NotificationManager)}.{nameof(NullOrEmpty)}", "Notification manager cannot be null");
        }
    }
}
