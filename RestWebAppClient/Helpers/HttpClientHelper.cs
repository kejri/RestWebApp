using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Helpers
{
    public static class HttpClientHelper
    {
        public static HttpClient GetClient(AuthenticationToken token)
        {
            var authValue = new AuthenticationHeaderValue(token.token_type, token.access_token);
            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }
    }
}
