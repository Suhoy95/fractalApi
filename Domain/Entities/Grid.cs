using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Grid
    {
        public int Id { get; set; }
        public String Slug { get; set; }
        public String Title { get; set; }
        public int Width { get; set; }
        public bool FixedWidth { get; set; }
        public Item[][] Items { get; set; }

        public String PageTitle { get; set; }
        public String PageDescription { get; set; }
        public String PageKeywords { get; set; }
    }

    public class PartialGrid
    {
        public int Id { get; set; }
        public String Slug { get; set; }
        public String Title { get; set; }
        public int Width { get; set; }
        public bool FixedWidth { get; set; }

        public String PageTitle { get; set; }
        public String PageDescription { get; set; }
        public String PageKeywords { get; set; }
    }
}
