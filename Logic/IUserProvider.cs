using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Logic
{
    public interface IUserProvider
    {
        Guid CurrentUserId();
    }
}
