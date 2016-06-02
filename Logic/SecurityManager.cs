using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Logic
{
    public static class SecurityManager
    {
        static IUserProvider m_UserProvider = null;

        public static void Init(IUserProvider a_UserProvider)
        {
            m_UserProvider = a_UserProvider;
        }

        public static Guid CurrentUser
        {
            get
            {
                if (m_UserProvider == null) throw new Exception("SecurityManager is not initialized");
                return m_UserProvider.CurrentUserId();
            }
        }
    }
}
