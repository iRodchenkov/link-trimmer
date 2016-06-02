using iRodchenkov.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace iRodchenkov.WebInterface.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("r")]
    public class RedirectController : ApiController
    {
        [Route("{id}")]
        [HttpGet]
        public RedirectResult Test(int id)
        {
            using (var trimmer = new LinkTrimmer())
            {
                var url = trimmer.GetOriginalUrl(id);
                return Redirect(url);
            }
        }
    }
}
