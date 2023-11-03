using HoaM.Domain.Exceptions;

namespace HoaM.Application.Exceptions
{
    public static class ApplicationErrors
    {
        public static class AssociationMember
        {
            public static Error NotFound => new($"{nameof(AssociationMember)}.{nameof(NotFound)}", "The specified member was not found.");
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
    }
}
