using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestWebAppClient.Startup))]
namespace RestWebAppClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
