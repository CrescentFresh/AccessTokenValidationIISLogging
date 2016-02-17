using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AccessTokenValidationIISLogging.IdentityServer3.Startup))]

namespace AccessTokenValidationIISLogging.IdentityServer3
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            JwtSecurityTokenHandler.OutboundClaimTypeMap = new Dictionary<string, string>();

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:49907/core",
                ValidationMode = ValidationMode.Local, // JWT
                TokenProvider = new OAuthBearerAuthenticationProvider()
                {
                    OnValidateIdentity = ctx =>
                    {
                        var claims = new List<Claim>(ctx.Ticket.Identity.Claims);
                        claims.Add(new Claim("name", "johnny"));
                        var identity = new ClaimsIdentity(claims, ctx.Ticket.Identity.AuthenticationType, "name", "role");
                        return Task.FromResult(ctx.Validated(identity));
                    }
                }
            });

            var config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}
