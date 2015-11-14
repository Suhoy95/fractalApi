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
        void Delete(int id);
        bool Exsist(int id);
        bool IsCorrectSlug(String slug, int id);

        Item Create(Item grid, String username);
        void Update(Item grid);

        void Update(PartialGrid grid);
    }
}
