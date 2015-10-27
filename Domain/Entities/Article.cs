using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article : Item
    {
        public String Slug { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Text { get; set; }
        public String PageTitle { get; set; }
        public String PageDescription { get; set; }
        public String PageText { get; set; }
    }
}
