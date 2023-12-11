using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Defines a member of the home owner's association
    /// </summary>
    public class AssociationMember : Entity<AssociationMemberId>, IMember, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="AssociationMember"/>
        /// </summary>
        public override AssociationMemberId Id { get; protected set; } = AssociationMemberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// First name of the <see cref="AssociationMember"/> 
        /// </summary>
        public FirstName FirstName { get; protected set; } = null!;

        /// <summary>
        /// Last name of the <see cref="AssociationMember"/>
        /// </summary>
        public LastName LastName { get; protected set; } = null!;

        /// <summary>
        /// Email details of the <see cref="AssociationMember"/>
        /// </summary>
        public Email? Email { get; protected set; }

        /// <summary>
        /// Contact numbers for the <see cref="AssociationMember"/>
        /// </summary>
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();
        private readonly List<PhoneNumber> _phoneNumbers = [];

        /// <summary>
        /// Collection of <see cref="Committee"/>s that the <see cref="AssociationMember"/> serves
        /// </summary>
        public IReadOnlyCollection<CommitteeAssignment> Commitments => _commitments.AsReadOnly();
        private readonly List<CommitteeAssignment> _commitments = [];

        /// <summary>
        /// Resendential address (within the community) of the <see cref="AssociationMember"/>
        /// </summary>
        public Residence? Residence { get; protected set; }

        /// <summary>
        /// <see cref="Notification"/>s delivered to this <see cref="AssociationMember"/>
        /// </summary>
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        private readonly List<Notification> _notifications = [];

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
        public bool IsDeleted => DeletionDate is not null;

        public Username DisplayName => Username.TryParse(Email?.Address.Value, out var displayName)
            ? displayName
            : Username.From($"{FirstName} {LastName}");

        private protected AssociationMember() { }

        protected AssociationMember(FirstName name, LastName surname)
        {
            FirstName = name;
            LastName = surname;
        }

        public static AssociationMember Create(FirstName name, LastName surname)
        {
            if (name is null) throw new DomainException(DomainErrors.AssociationMember.FirstNameNullOrEmpty);

            if (surname is null) throw new DomainException(DomainErrors.AssociationMember.LastNameNullOrEmpty);

            return new() { FirstName = name, LastName = surname };
        }

        public AssociationMember WithEmailAddress(EmailAddress emailAddress)
        {
            if (emailAddress is null) throw new DomainException(DomainErrors.Email.AddressNullOrEmpty);

            Email = Email.Create(emailAddress);

            return this;
        }

        public AssociationMember WithResidence(Residence residence)
        {
            if (residence is null) throw new DomainException(DomainErrors.Parcel.NullOrEmpty);

            Residence = residence;

            return this;
        }

        public AssociationMember WithPhoneNumbers(params PhoneNumber[] numbers)
        {
            if (numbers is null || numbers.Length == 0) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            _phoneNumbers.AddRange(numbers);

            return this;
        }

        public AssociationMember AddPhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber is null) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            if (_phoneNumbers.Exists(p => p.Type == phoneNumber.Type)) throw new DomainException(DomainErrors.PhoneNumber.DuplicateType);

            _phoneNumbers.Add(phoneNumber);

            return this;
        }

        public void RemovePhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber is null) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            _phoneNumbers.Remove(phoneNumber);
        }

        public void EditFirstName(FirstName name)
        {
            if (name is null) throw new DomainException(DomainErrors.AssociationMember.FirstNameNullOrEmpty);

            if (name == FirstName) return;

            FirstName = name;
        }

        public void EditLastName(LastName surname)
        {
            if (surname is null) throw new DomainException(DomainErrors.AssociationMember.LastNameNullOrEmpty);

            if (surname == LastName) return;

            LastName = surname;
        }

        public void RemoveResidence() => Residence = null;
    }
}
