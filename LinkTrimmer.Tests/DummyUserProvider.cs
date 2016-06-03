using iRodchenkov.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.LinkTrimmer.Tests
{
    public sealed class DummyUserProvider : IUserProvider
    {
        public DummyUserProvider()
        {
            m_CurrentUserId = Guid.NewGuid();
        }

        Guid m_CurrentUserId;

        public Guid CurrentUserId()
        {
            return m_CurrentUserId;
        }
    }
}
