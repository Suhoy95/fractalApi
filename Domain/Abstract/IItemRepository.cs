using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IItemRepository
    {
        void UpdateCoord(int[][] coord);
    }
}
