using HoaM.Domain.Exceptions;

namespace HoaM.Application.Exceptions
{
    public static class ApplicationErrors
    {
        public static class AssociationMember
        {
            public static Error NotFound => new($"{nameof(AssociationMember)}.{nameof(NotFound)}", "The specified member was not found.");
            public static Error AlreadyDeleted => new($"{nameof(AssociationMember)}.{nameof(AlreadyDeleted)}", "This member no longer exists.");
            public static Error DuplicatePhone => new($"{nameof(AssociationMember)}.{nameof(DuplicatePhone)}", "A phone number of this type already exists.");

        }

        public static class Community
        {
            public static Error NotFound => new($"{nameof(Community)}.{nameof(NotFound)}", "The specified community was not found.");
            public static Error DuplicateName => new($"{nameof(Community)}.{nameof(DuplicateName)}", "A community with this name already exists.");
        }

        public static class Committee
        {
            public static Error NotFound => new($"{nameof(Committee)}.{nameof(NotFound)}", "The specified committee was not found.");
            public static Error AlreadyDeleted => new($"{nameof(Committee)}.{nameof(AlreadyDeleted)}", "This committee no longer exists.");
            public static Error AlreadyDissolved => new($"{nameof(Committee)}.{nameof(AlreadyDissolved)}", "This committee has already been dissolved.");
            public static Error DuplicateName => new($"{nameof(Committee)}.{nameof(DuplicateName)}", "A committee with this name has already been established.");
        }

        public static class Email
        {
            public static Error NotFound => new($"{nameof(Email)}.{nameof(NotFound)}", "Email was not found.");
            public static Error AlreadyVerified => new($"{nameof(Email)}.{nameof(AlreadyVerified)}", "The specified email has already been verified.");
        }

        public static class PhoneNumber
        {
            public static Error NotFound => new($"{nameof(PhoneNumber)}.{nameof(NotFound)}", "Member phone number was not found.");
            public static Error LimitReached => new($"{nameof(PhoneNumber)}.{nameof(LimitReached)}", "Maximum allowed phone numbers exists.");
            public static Error LimitExceeded => new($"{nameof(PhoneNumber)}.{nameof(LimitExceeded)}", "Collection exceeds the maximum phone numbers allowed.");
            public static Error DuplicateTypeFound => new($"{nameof(PhoneNumber)}.{nameof(DuplicateTypeFound)}", "Duplicate phone type not allowed.");

        }

        public static class Residence
        {
            public static Error NotFound => new($"{nameof(Residence)}.{nameof(NotFound)}", "Member residnece was not found.");
            public static Error AlreadyDeleted => new($"{nameof(Residence)}.{nameof(AlreadyDeleted)}", "This residence no longer exists.");
        }
    }
}
