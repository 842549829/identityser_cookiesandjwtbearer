using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controller
{
    [Route("cookietest")]
    [Authorize]
    public class CookieTestController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            var refresh_token = this.User.Claims.FirstOrDefault(d => d.Type == "refresh_token")?.Value;
            if (refresh_token != null)
            {
                // 刷新 access_token
            }
            // 根据token刷新 acco
            return Ok("CookieTest");
        }
    }
}