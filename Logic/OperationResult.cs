using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Logic
{
    public sealed class OperationResult
    {
        List<string> m_Errors = new List<string>();

        public void AddError(string a_Error)
        {
            m_Errors.Add(a_Error);
        }

        public bool HasErrors
        {
            get
            {
                return m_Errors.Any();
            }
        }

        public string AllErrors
        {
            get
            {
                return string.Join("; ", m_Errors);
            }
        }

    }
}
