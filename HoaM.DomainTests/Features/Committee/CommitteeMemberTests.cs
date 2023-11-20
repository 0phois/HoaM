using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaM.Domain.UnitTests
{
    public class CommitteeMemberTests
    {
        [Fact]
        public void CreateCommitteeMember_WithValidData_ReturnsCommitteeMemberInstance()
        {
            // Arrange
            var firstName = new FirstName("John");
            var lastName = new LastName("Doe");

            // Act
            var committeeMember = CommitteeMember.Create(firstName, lastName);

            // Assert
            Assert.NotNull(committeeMember);
            Assert.Equal(firstName, committeeMember.FirstName);
            Assert.Equal(lastName, committeeMember.LastName);
            Assert.Equal(CommitteeRole.Member, committeeMember.Position);
        }

        [Fact]
        public void CreateCommitteeMember_WithRole_ReturnsCommitteeMemberWithSpecifiedRole()
        {
            // Arrange
            var firstName = new FirstName("Jane");
            var lastName = new LastName("Doe");
            var role = CommitteeRole.Chairman;

            // Act
            var committeeMember = CommitteeMember.Create(firstName, lastName, role);

            // Assert
            Assert.Equal(role, committeeMember.Position);
        }

        [Fact]
        public void CreateCommitteeMemberFromAssociationMember_WithValidData_ReturnsCommitteeMemberInstance()
        {
            // Arrange
            var associationMember = AssociationMember.Create(new FirstName("Alice"), new LastName("Johnson"));

            // Act
            var committeeMember = CommitteeMember.CreateFrom(associationMember);

            // Assert
            Assert.NotNull(committeeMember);
            Assert.Equal(associationMember.FirstName, committeeMember.FirstName);
            Assert.Equal(associationMember.LastName, committeeMember.LastName);
            Assert.Equal(CommitteeRole.Member, committeeMember.Position);
        }

        [Fact]
        public void CreateCommitteeMemberFromAssociationMember_WithRole_ReturnsCommitteeMemberWithSpecifiedRole()
        {
            // Arrange
            var associationMember = AssociationMember.Create(new FirstName("Bob"), new LastName("Smith"));
            var role = CommitteeRole.Secretary;

            // Act
            var committeeMember = CommitteeMember.CreateFrom(associationMember, role);

            // Assert
            Assert.Equal(role, committeeMember.Position);
        }
    }
}
