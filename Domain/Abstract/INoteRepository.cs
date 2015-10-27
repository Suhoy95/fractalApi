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
        Note Create(Note note);
        void Update(Note note);
        void Delete(int Id);

        bool Exist(int Id);
    }
}
