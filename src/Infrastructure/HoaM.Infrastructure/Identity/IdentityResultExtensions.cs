using HoaM.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HoaM.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Results.Success()
                : Results.Failed(string.Join("\n", $"• {result.Errors.Select(e => e.Description)}"));
        }
    }
}
