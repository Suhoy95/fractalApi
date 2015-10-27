using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IGridRepository
    {
        Grid GetGrid(String slug);
        bool DeleteGrid(String slug);
        bool Exsist(String slug);

        GridItem Create(GridItem grid);
        void Update(GridItem grid);

        void Update(PartialGrid grid);
    }
}
