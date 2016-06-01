using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkTrimmer
{
    public class CookieUserProvider : IUserProvider
    {
        const string c_CoockieName = "link.trimmer.cookie";
        const string c_UserIdField = "UserId";
        const int c_DaysToExpire = 30;

        public Guid CurrentUserId()
        {
            var coockie = HttpContext.Current.Response.Cookies[c_CoockieName];

            if (coockie == null)
            {
                coockie = HttpContext.Current.Request.Cookies[c_CoockieName];

                if (coockie == null)
                {
                    var newId = Guid.NewGuid();
                    coockie = new HttpCookie(c_CoockieName);
                    coockie.Values.Add(c_UserIdField, newId.ToString());
                }

                HttpContext.Current.Response.Cookies.Add(coockie);
            }

            coockie.Expires = DateTime.Now.AddDays(c_DaysToExpire);

            return Guid.Parse(coockie.Values[c_UserIdField]);
        }


    }
}