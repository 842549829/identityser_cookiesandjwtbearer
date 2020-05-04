using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Controller
{
    [Route("default")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DefaultController :  Microsoft.AspNetCore.Mvc.Controller
    {
        public async Task<IActionResult> Index()
        {

            var bearerToken = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(bearerToken);
            var claims = jwtToken.Claims;

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var item in claims)
            {
                claimsIdentity.AddClaim(item);
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok("default");
        }
    }
}
