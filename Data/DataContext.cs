using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=LinkTrimmerConnectionString")
        {

        }

        public DbSet<LinkData> Links { get; set; }
    }
}
