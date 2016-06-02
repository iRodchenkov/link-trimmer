using iRodchenkov.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iRodchenkov.Logic
{
    public sealed class LinkInfo
    {
        const string c_OfflineUrlPrefix = "offline";

        string UrlPrefix()
        {
            if (HttpContext.Current == null) return c_OfflineUrlPrefix;
            else return string.Format(@"{0}://{1}/r", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
        }

        string FormatUrl(LinkData a_Data)
        {
            return string.Format("{0}/{1}", UrlPrefix(), a_Data.Id);
        }

        public LinkInfo(LinkData a_Data)
        {
            SourceUrl = a_Data.Source;
            TrimmedUrl = FormatUrl(a_Data);
            CreatedAt = a_Data.CreatedAt;
            Clicks = a_Data.Clicks;
        }

        public string SourceUrl { get; private set; }
        public string TrimmedUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int Clicks { get; private set; }
    }
}
