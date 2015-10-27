using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class Note : Item
    {
        public String Title { get; set; }
        public String Text { get; set; }
    }
}
