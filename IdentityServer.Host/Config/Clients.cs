using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.Host.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Client Credentials Flow Client",
                    Enabled = true,
                    ClientId = "client",
                    
                    ClientSecrets = new List<Secret>
                    { 
                        new Secret("secret".Sha256())
                    },

                    Flow = Flows.ClientCredentials,

                    AllowedScopes = new List<string> 
                    {
                        "read", 
                        "write"
                    },
                    
                    Claims = new List<Claim>
                    {
                        new Claim("client_type", "headless")
                    },
                    PrefixClientClaims = false
                },
            };
        }
    }
}