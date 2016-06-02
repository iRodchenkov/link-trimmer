using iRodchenkov.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iRodchenkov.WebInterface
{
    public class CookieUserProvider : IUserProvider
    {
        const string c_CoockieName = "link.trimmer.cookie";
        const string c_UserIdField = "LinkTrimmerUserId";
        const int c_DaysToExpire = 30;

        public Guid CurrentUserId()
        {
            var userId = CreateOrUpdateCoockie();

            return userId;
        }

        Guid CreateOrUpdateCoockie()
        {
            Guid res;
            var coockie = HttpContext.Current.Request.Cookies[c_CoockieName];

            if (coockie == null)
            {
                res = Guid.NewGuid();
                coockie = new HttpCookie(c_CoockieName);
                coockie.Value = res.ToString();
            }
            else
            {
                res = Guid.Parse(coockie.Value);
            }

            coockie.Expires = DateTime.Now.AddDays(c_DaysToExpire);
            HttpContext.Current.Response.Cookies.Add(coockie);

            return res;
        }


    }
}