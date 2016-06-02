using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRodchenkov.Data
{
    public sealed class LinkData
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        [Index("Index_Links_Created")]
        public Guid CreatedBy { get; set; }
        public int Clicks { get; set; }
    }
}
