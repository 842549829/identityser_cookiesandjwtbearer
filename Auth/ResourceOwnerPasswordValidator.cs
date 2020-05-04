using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Holder.ERP.Auth.Identity.WebApi
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context) => 
            await Task.Run(() =>
            {
                var claims = GetUserClaims();
                context.Result = new GrantValidationResult(
                    subject: "1",
                    authenticationMethod: "custom",
                    claims: claims);
            });

        public static ICollection<Claim> GetUserClaims()
        {
            return new List<Claim>
            {
                new Claim("uid", "1"),
                new Claim("uname","2"),
                new Claim("eid", "3")
            };
        }
    }
}
