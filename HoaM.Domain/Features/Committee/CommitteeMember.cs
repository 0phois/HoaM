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
        private readonly List<Committee> _committees = new();

        private CommitteeMember(FirstName name, LastName surname) : base(name, surname) { }

        public static CommitteeMember Createreate(FirstName name, LastName surname)
        {
            return new(name, surname);
        }

        public static CommitteeMember CreateFrom(AssociationMember member, CommitteeRole role)
        {
            return new(member.FirstName, member.LastName) 
            { 
                Position = role,
                Email = member.Email,
                Residence = member.Residence,
                PhoneNumbers = member.PhoneNumbers,
            };
        }
    }
}
