using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccessTokenValidationIISLogging.IdentityServer3.Controllers
{
    [Authorize]
    [RoutePrefix("")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Index()
        {
            return Ok(GetType().FullName);
        }
    }
}
