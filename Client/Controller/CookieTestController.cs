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
            return Ok(" CookieTest");
        }
    }
}