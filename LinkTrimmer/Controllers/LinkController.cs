using iRodchenkov.Logic;
using iRodchenkov.WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iRodchenkov.WebInterface.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/link")]
    public sealed class LinkController : ApiController
    {
        int c_ItemsOnPage = 5;

        [Route("trim")]
        [HttpPost]
        public string Trim([FromBody]TrimRequest value)
        {
            using (var trimmer = new LinkTrimmer())
            {
                return trimmer.CreateLink(value.SourceUrl).TrimmedUrl;
            }
        }

        [Route("history")]
        [HttpPost]
        public object History([FromBody]HistoryRequest value)
        {
            using (var trimmer = new LinkTrimmer())
            {
                int total;

                var res = trimmer.HistoryForCurrentUser((value.Page - 1) * c_ItemsOnPage, c_ItemsOnPage, out total);

                return new
                {
                    Items = res,
                };
            }
        }
    }
}
