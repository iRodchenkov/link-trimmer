using Data;
using LinkTrimmer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LinkTrimmer.Controllers
{
    [AllowAnonymous ]
    [RoutePrefix("api/link-trimmer")]
    public class LinkTrimmerController : ApiController
    {
        [Route("trim")]
        [HttpPost]
        public string Trim([FromBody]TrimRequest value)
        {
            

            using (var context = new DataContext())
            {
                var link = new LinkData
                {
                    Source = value.BaseUrl,
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                };

                context.Links.Add(link);

                int i = link.Id;
            }

            return string.Format("{0} {1}", value == null ? "" : value.BaseUrl, "Hello, World");
        }
    }
}
