using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace ERP.WebApi.Extensions
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// Gets Id of current user
        /// </summary>
        public static Guid GetCurrentUserId(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;
            var claim = claimsPrincipal?.Claims.Single(c => c.Type == /*ClaimEnums.UserId*/"");

            return Guid.Parse(claim.Value);
        }
    }
}