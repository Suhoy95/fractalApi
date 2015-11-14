using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPermissionChecker
    {
        bool GridAllowed(string login, int listId);
        bool ItemAllowed(string login, Item item);
        bool CoordsAllowed(string login, Coords coords);
    }
}
