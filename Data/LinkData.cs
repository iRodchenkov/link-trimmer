using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Data
{
    public class LinkData
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public int Clicks { get; set; }
    }
}
