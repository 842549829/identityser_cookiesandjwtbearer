using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace Auth
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api","this is api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="mvc_imp",
                    ClientName="Mvc_Name",
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials, 
                    ////设置是否要授权
                    //RequireConsent=false,
                    
                    //指定允许令牌或授权码返回的地址（URL）
                    //RedirectUris={ "http://www.b.net:5001/signin-oidc","http://www.a.cn:5002/signin-oidc" },
                    //指定允许注销后返回的地址(URL)，这里写两个客户端
                    //PostLogoutRedirectUris={ "http://www.b.net:5001/signout-callback-oidc","http://www.a.cn:5002/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.OfflineAccess,
                        "api"
                    },
                }
             };
        }

    }
}