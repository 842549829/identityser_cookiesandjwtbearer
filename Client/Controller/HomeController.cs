using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controller
{
    [Route("/home")]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory  httpClientFactory) 
        {
            _client = httpClientFactory.CreateClient();
        }

        [HttpGet("index")]
        public IActionResult Index(string returnUrl)
        {
            return Ok("1");
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var disco = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://localhost:51376",
                Policy =
                {
                    RequireHttps = false
                }
            });
            if (disco.IsError)
            {
                throw new InvalidOperationException();
            }

            var parameters = new Dictionary<string, string>
            {
                { "eid", "xxxxxxxxxxxxxx" }
            };

            // request token
            var tokenResponse = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "mvc_imp",
                ClientSecret = "secret",
                UserName = "12",
                Password = "2",
                Parameters = parameters
            });

            if (tokenResponse.IsError)
            {
                throw new InvalidOperationException();
            }

            return Ok(tokenResponse.AccessToken);
        }
    }
}