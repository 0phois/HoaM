using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an association member within a community or organization.
    /// </summary>
    public class AssociationMember : Entity<AssociationMemberId>, IMember, ISoftDelete
    {
        /// <inheritdoc/>
        public override AssociationMemberId Id { get; protected set; } = AssociationMemberId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the first name of the association member.
        /// </summary>
        public FirstName FirstName { get; protected set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the association member.
        /// </summary>
        public LastName LastName { get; protected set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the association member.
        /// </summary>
        public Email? Email { get; protected set; }

        /// <summary>
        /// Gets the read-only collection of phone numbers associated with the association member.
        /// </summary>
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();
        private readonly List<PhoneNumber> _phoneNumbers = [];

        /// <summary>
        /// Gets the read-only collection of committee assignments for the association member.
        /// </summary>
        public IReadOnlyCollection<CommitteeAssignment> Commitments => _commitments.AsReadOnly();
        private readonly List<CommitteeAssignment> _commitments = [];

        /// <summary>
        /// Gets or sets the residence information of the association member.
        /// </summary>
        public Residence? Residence { get; protected set; }

        /// <summary>
        /// Gets the read-only collection of notifications for the association member.
        /// </summary>
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        private readonly List<Notification> _notifications = [];

        /// <inheritdoc/>
        public AssociationMemberId? DeletedBy { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset? DeletionDate { get; set; }

        /// <inheritdoc/>
        public bool IsDeleted => DeletionDate is not null;

        /// <summary>
        /// Gets the display name or username of the association member.
        /// </summary>
        public Username DisplayName => Username.TryParse(Email?.Address.Value, out var displayName)
            ? displayName
            : Username.From($"{FirstName} {LastName}");

        /// <summary>
        /// Private protected constructor for <see cref="AssociationMember"/> to enforce creation through static methods.
        /// </summary>
        private protected AssociationMember() { }

        /// <summary>
        /// Protected constructor for creating an <see cref="AssociationMember"/> with a first name and last name.
        /// </summary>
        /// <param name="name">The first name of the association member.</param>
        /// <param name="surname">The last name of the association member.</param>
        protected AssociationMember(FirstName name, LastName surname)
        {
            FirstName = name;
            LastName = surname;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AssociationMember"/>.
        /// </summary>
        /// <param name="name">The first name of the association member.</param>
        /// <param name="surname">The last name of the association member.</param>
        /// <returns>The created <see cref="AssociationMember"/> instance.</returns>
        public static AssociationMember Create(FirstName name, LastName surname)
        {
            if (name is null) throw new DomainException(DomainErrors.AssociationMember.FirstNameNullOrEmpty);

            if (surname is null) throw new DomainException(DomainErrors.AssociationMember.LastNameNullOrEmpty);

            return new() { FirstName = name, LastName = surname };
        }

        /// <summary>
        /// Adds an email address to the association member.
        /// </summary>
        /// <param name="emailAddress">The email address to add.</param>
        /// <returns>The modified association member.</returns>
        public AssociationMember WithEmailAddress(EmailAddress emailAddress)
        {
            if (emailAddress is null) throw new DomainException(DomainErrors.Email.AddressNullOrEmpty);

            Email = Email.Create(emailAddress);

            return this;
        }

        /// <summary>
        /// Adds residence information to the association member.
        /// </summary>
        /// <param name="residence">The residence information to add.</param>
        /// <returns>The modified association member.</returns>
        public AssociationMember WithResidence(Residence residence)
        {
            if (residence is null) throw new DomainException(DomainErrors.Parcel.NullOrEmpty);

            Residence = residence;

            return this;
        }

        /// <summary>
        /// Adds phone numbers to the association member.
        /// </summary>
        /// <param name="numbers">The phone numbers to add.</param>
        /// <returns>The modified association member.</returns>
        public AssociationMember WithPhoneNumbers(params PhoneNumber[] numbers)
        {
            if (numbers is null || numbers.Length == 0) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            _phoneNumbers.AddRange(numbers);

            return this;
        }

        /// <summary>
        /// Adds a phone number to the association member.
        /// </summary>
        /// <param name="phoneNumber">The phone number to add.</param>
        /// <returns>The modified association member.</returns>
        public AssociationMember AddPhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber is null) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            if (_phoneNumbers.Exists(p => p.Type == phoneNumber.Type)) throw new DomainException(DomainErrors.PhoneNumber.DuplicateType);

            _phoneNumbers.Add(phoneNumber);

            return this;
        }

        /// <summary>
        /// Removes a phone number from the association member.
        /// </summary>
        /// <param name="phoneNumber">The phone number to remove.</param>
        public void RemovePhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber is null) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            _phoneNumbers.Remove(phoneNumber);
        }

        /// <summary>
        /// Edits the first name of the association member.
        /// </summary>
        /// <param name="name">The new first name.</param>
        public void EditFirstName(FirstName name)
        {
            if (name is null) throw new DomainException(DomainErrors.AssociationMember.FirstNameNullOrEmpty);

            if (name == FirstName) return;

            FirstName = name;
        }

        /// <summary>
        /// Edits the last name of the association member.
        /// </summary>
        /// <param name="surname">The new last name.</param>
        public void EditLastName(LastName surname)
        {
            if (surname is null) throw new DomainException(DomainErrors.AssociationMember.LastNameNullOrEmpty);

            if (surname == LastName) return;

            LastName = surname;
        }

        /// <summary>
        /// Removes residence information from the association member.
        /// </summary>
        public void RemoveResidence() => Residence = null;
    }

}
