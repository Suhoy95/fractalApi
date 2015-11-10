using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Item
    {
        public int id { get; set; }
        public int gridId { get; set; }
        public String type { get; set; }
        public int[] analogy { get; set; }
        public int[] sup { get; set; }
        public int[] sub { get; set; }

        public String slug { get; set; }

        public String title { get; set; }
        public String text { get; set; }
    }
}
