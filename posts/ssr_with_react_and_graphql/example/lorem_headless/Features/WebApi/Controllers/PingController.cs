using EPiServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace lorem_headless.Features.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class PingController
        : ApiController
    {
        public PingController(IContentLoader loader)
        {
        }

        [HttpGet()]
        [Route("ping")]
        public string Ping()
        {
            return "Ok";
        }
    }
}