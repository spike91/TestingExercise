using Api_App.Servises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_App.Controllers
{
    [RoutePrefix("api/main")]
    public class MainController : ApiController
    {
        [Route("SetToken/{token}")]
        [AcceptVerbs("GET", "POST")]
        public void SetToken(Guid token)
        {
            HttpClientService.SetToken(token);
        }

        [Route("GetToken")]
        [AcceptVerbs("GET", "POST")]
        public Guid GetToken()
        {
           return HttpClientService.Token;
        }
    }
}
