using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Api_App.Servises
{
    public class HttpClientService : HttpClient
    {
        private static readonly HttpClientService instance = new HttpClientService();
        public static HttpClientService Instance { get { return instance; } }
        public static Guid Token { get; set; }

        static HttpClientService() { }
        private HttpClientService() : base() { }

        public static void SetToken(Guid token) {
            Token = token;
            instance.DefaultRequestHeaders.Clear();
            instance.DefaultRequestHeaders.TryAddWithoutValidation("authorization", token.ToString());
        }
    }
}