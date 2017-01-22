using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestWebAppClient.Helpers
{
    public class SecurityHelper
    {
        public static AuthenticationToken GetToken(SessionHelper sessionHelper)
        {
            var token = sessionHelper.Token;
            //Pokud token neni v session, pak ho nacti ze service
            if (token == null)
            {
                using (var client = new HttpClient { BaseAddress = new Uri(Const.UrlService) })
                {
                    token = client.PostAsync("Token",
                        new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string,string>("grant_type","password"),
                        new KeyValuePair<string,string>("username", Const.Username),
                        new KeyValuePair<string,string>("password",Const.Password)
                        })).Result.Content.ReadAsAsync<AuthenticationToken>().Result;

                    if (String.IsNullOrEmpty(token.access_token))
                    {
                        throw new Exception("Chybné uživatelské jméno nebo heslo.");
                    }
                }
                sessionHelper.Token = token;
            }
            return token;
        }
    }
}
