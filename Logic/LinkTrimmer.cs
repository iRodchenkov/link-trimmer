using iRodchenkov.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iRodchenkov.Logic
{
    public sealed class LinkTrimmer : IDisposable
    {
        DataContext m_DataCotext = null;

        public LinkTrimmer()
        {
            m_DataCotext = new DataContext();
        }

        public void Dispose()
        {
            if (m_DataCotext != null) m_DataCotext.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_SourceUrl">Original Url</param>
        /// <returns>Trimmed Url</returns>
        public LinkInfo CreateLink(string a_SourceUrl)
        {
            var link = new LinkData
            {
                Source = a_SourceUrl,
                CreatedAt = DateTime.Now,
                CreatedBy = SecurityManager.CurrentUser,
            };

            m_DataCotext.Links.Add(link);
            m_DataCotext.SaveChanges();

            return new LinkInfo(link);
        }

        public LinkInfo[] HistoryForCurrentUser(int a_Skip, int a_Take, out int a_Total)
        {
            var q = m_DataCotext.Links.Where(x => x.CreatedBy == SecurityManager.CurrentUser);
            a_Total = q.Count();

            return q.OrderByDescending(x => x.CreatedAt).Skip(a_Skip).Take(a_Take).ToArray().Select(x => new LinkInfo(x)).ToArray();
        }
    }
}
