using iRodchenkov.Logic;
using iRodchenkov.WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace iRodchenkov.WebInterface.Controllers
{
    [AllowAnonymous ]
    [RoutePrefix("api/link-trimmer")]
    public class LinkTrimmerController : ApiController
    {
        [Route("trim")]
        [HttpPost]
        public string Trim([FromBody]TrimRequest value)
        {
            using (var trimmer = new LinkTrimmer())
            {
                return trimmer.CreateLink(value.SourceUrl);
            }
        }
    }
}
