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
        Grid Get(String slug);
        bool Delete(int id);
        bool Exsist(int id);

        GridItem Create(GridItem grid);
        void Update(GridItem grid);

        void Update(PartialGrid grid);
    }
}
