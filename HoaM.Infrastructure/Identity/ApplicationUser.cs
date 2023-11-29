using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.AspNetCore.Identity;

namespace HoaM.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<AssociationMemberId>, IMember
    {
        public Username DisplayName => Username.From(UserName!);

        public ApplicationUser(string username) => UserName = username;
    }
}
