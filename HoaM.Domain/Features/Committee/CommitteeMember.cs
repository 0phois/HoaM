using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class CommitteeMember : AssociationMember
    {
        /// <summary>
        /// <see cref="CommitteeMember"/>'s role in the <seealso cref="Committee"/>
        /// </summary>
        public CommitteeRole Position { get; set; } = null!;

        /// <summary>
        /// <see cref="Committee"/> to which this <seealso cref="CommitteeMember"/> belongs
        /// </summary>
        public IReadOnlyCollection<Committee> Committees => _committees.AsReadOnly();
        private readonly List<Committee> _committees = [];

        private CommitteeMember() { }

        private CommitteeMember(FirstName name, LastName surname) : base(name, surname) { }

        public static CommitteeMember Create(FirstName name, LastName surname, CommitteeRole? role = null)
        {
            if (name is null) throw new DomainException(DomainErrors.AssociationMember.FirstNameNullOrEmpty);

            if (surname is null) throw new DomainException(DomainErrors.AssociationMember.LastNameNullOrEmpty);

            return new(name, surname) { Position = role ?? CommitteeRole.Member };
        }

        public static CommitteeMember CreateFrom(AssociationMember member, CommitteeRole? role = null)
        {
            if (member is null) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            return new(member.FirstName, member.LastName)
            {
                Position = role ?? CommitteeRole.Member,
                Email = member.Email,
                Residence = member.Residence,
                PhoneNumbers = member.PhoneNumbers,
            };
        }
    }
}
