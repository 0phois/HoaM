using HoaM.Domain;
using Microsoft.AspNetCore.Identity;

namespace HoaM.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<AssociationMemberId>, IMember
    {
        public Username DisplayName => Username.From(UserName!);

        public ApplicationUser() { }

        public ApplicationUser(string username) => UserName = username;
    }
}
