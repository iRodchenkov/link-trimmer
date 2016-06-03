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
        int c_Delta = 2;

        [Route("trim")]
        [HttpPost]
        public object Trim([FromBody]TrimRequest value)
        {
            using (var trimmer = new LinkTrimmer())
            {
                var r = new OperationResult();
                var res = trimmer.CreateLink(value.SourceUrl, r);

                return new
                {
                    Link = res,
                    R = r,
                };
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

                int pages = total / c_ItemsOnPage;
                if (total % c_ItemsOnPage != 0) ++pages;

                var pagination = new List<int>();
                for (int p = Math.Max(1, value.Page - c_Delta); p <= Math.Min(value.Page + c_Delta, pages); ++p) pagination.Add(p);

                return new
                {
                    Items = res,
                    Pages = pages,
                    Pagination = pagination.Select(p => new { Page = p }).ToArray(),
                };
            }
        }
    }
}
