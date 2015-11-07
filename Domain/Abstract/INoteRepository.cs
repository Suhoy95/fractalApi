using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface INoteRepository
    {
        Item Create(Item note);
        void Update(Item note);
        void Delete(int Id);

        bool Exist(int Id);
    }
}
