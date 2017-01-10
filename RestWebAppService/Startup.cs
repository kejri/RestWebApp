using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(RestWebAppService.Startup))]

namespace RestWebAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            /*var config = new HttpConfiguration();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;

            app.UseWebApi(config);*/
        }
    }
}
