using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an email entity with verification status.
    /// </summary>
    public sealed class Email : Entity<EmailId>
    {
        /// <inheritdoc/>
        public override EmailId Id { get; protected set; } = EmailId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets a value indicating whether the email address is verified.
        /// </summary>
        public bool IsVerified { get; private set; }

        /// <summary>
        /// Gets the email address associated with the entity.
        /// </summary>
        public required EmailAddress Address { get; init; }

        /// <summary>
        /// Private constructor for creating an instance of <see cref="Email"/>.
        /// </summary>
        private Email() { }

        /// <summary>
        /// Creates a new instance of <see cref="Email"/> with the specified email address.
        /// </summary>
        /// <param name="emailAddress">The email address to associate with the entity.</param>
        /// <returns>The created <see cref="Email"/> instance.</returns>
        public static Email Create(EmailAddress emailAddress)
        {
            if (emailAddress is null) throw new DomainException(DomainErrors.Email.AddressNullOrEmpty);

            return new() { Address = emailAddress };
        }

        /// <summary>
        /// Marks the email address as verified.
        /// </summary>
        public void Verify() => IsVerified = true;
    }

}
