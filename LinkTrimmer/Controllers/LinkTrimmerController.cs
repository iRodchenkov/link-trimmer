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
            var coockieName = "link.trimmer.cookie";

            var coockie = HttpContext.Current.Request.Cookies[coockieName];

            if (coockie == null) coockie = new HttpCookie(coockieName, Guid.NewGuid().ToString());
            
            //coockie.Values.A

            coockie.Expires = DateTime.Now.AddHours(1);
            HttpContext.Current.Response.Cookies.Add(coockie);

            return string.Format("{0} {1}", value == null ? "" : value.BaseUrl, "Hello, World");
        }
    }
}
