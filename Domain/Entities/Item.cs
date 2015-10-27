using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int[] Analogy { get; set; }
        public int[] Sup { get; set; }
        public int[] Sub { get; set; }
    }
}
