using System;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Resources;
using Microsoft.Owin;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(IdentityServer.Host.Startup))]

namespace IdentityServer.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.Trace()
                            .CreateLogger();

            app.Map("/core", coreApp =>
            {
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(Config.Users.Get())
                    .UseInMemoryClients(Config.Clients.Get())
                    .UseInMemoryScopes(Config.Scopes.Get());

                factory.ConfigureClientStoreCache();
                factory.ConfigureScopeStoreCache();
                factory.ConfigureUserServiceCache();

                var idsrvOptions = new IdentityServerOptions
                {
                    Factory = factory,
                    SigningCertificate = Config.Cert.Load(),
                    RequireSsl = false,
                    Endpoints = new EndpointOptions
                    {
                        // replaced by the introspection endpoint in v2.2
                        EnableAccessTokenValidationEndpoint = false
                    }
                };

                coreApp.UseIdentityServer(idsrvOptions);
            });
        }
    }
}
