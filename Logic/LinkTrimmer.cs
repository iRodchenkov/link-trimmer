using iRodchenkov.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Logic
{
    public class LinkTrimmer : IDisposable
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
        public string CreateLink(string a_SourceUrl)
        {
            var link = new LinkData
            {
                Source = a_SourceUrl,
                CreatedAt = DateTime.Now,
                CreatedBy = SecurityManager.CurrentUser,
            };

            m_DataCotext.Links.Add(link);
            m_DataCotext.SaveChanges();

            return string.Format("http://localhost:3476/{0}", link.Id);
        }
    }
}
