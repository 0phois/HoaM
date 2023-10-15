using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Defines a member of the home owner's association
    /// </summary>
    public class AssociationMember : Entity<AssociationMemberId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="AssociationMember"/>
        /// </summary>
        public override AssociationMemberId Id => AssociationMemberId.From(NewId.Next().ToGuid());

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
        public List<PhoneNumber> PhoneNumbers { get; protected set; } = new List<PhoneNumber>();

        /// <summary>
        /// Resendential address (within the community) of the <see cref="AssociationMember"/>
        /// </summary>
        public Residence? Residence { get; protected set; }

        /// <summary>
        /// <see cref="Notification"/>s delivered to this <see cref="AssociationMember"/>
        /// </summary>
        public IReadOnlyCollection<Notification> Notifications { get; } = new List<Notification>().AsReadOnly();

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }

        private AssociationMember() { }
        
        protected AssociationMember(FirstName name, LastName surname)
        {
            FirstName = name;
            LastName = surname;
        }

        public static AssociationMember Create(FirstName name, LastName surname)
        {
            return new() { FirstName = name, LastName = surname };
        }

        public AssociationMember WithEmailAddress(EmailAddress emailAddress) 
        {
            Email = Email.Create(emailAddress);

            return this;
        }

        public AssociationMember WithResidence(Residence residence)
        {
            if (Residence is not null) throw new DomainException(DomainErrors.AssociationMember.DuplicateResidenceAssignment);

            Residence = residence;

            return this;
        }

        public AssociationMember WithPhoneNumbers(params PhoneNumber[] numbers)
        {
            if (numbers is null || numbers.Length == 0) throw new DomainException(DomainErrors.PhoneNumber.NullOrEmpty);

            PhoneNumbers.AddRange(numbers); 
            
            return this;
        }

        public AssociationMember AddPhoneNumber(PhoneNumber phoneNumber)
        {
            if (PhoneNumbers.Any(p => p.Type == phoneNumber.Type)) throw new DomainException(DomainErrors.PhoneNumber.DuplicateType);
        
            PhoneNumbers.Add(phoneNumber);

            return this;
        }

        public void EditFirstName(FirstName name)
        {
            if (name == FirstName) return;

            FirstName = name;
        }

        public void EditLastName(LastName surname) 
        {
            if (surname == LastName) return;
            
            LastName = surname;
        }

        public void UpdateResidence(Residence residence)
        {
            if (residence.Equals(Residence)) return;

            Residence = residence;
        }

        public void RemoveResidence() => Residence = null;
    }
}

//TODO - User Manager
/*
 Create a service that facilitates editing email, phone numbers etc
 */
