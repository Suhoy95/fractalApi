using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    interface INoteRepository
    {
        public Note Get(int Id);
        public Note Create(Note note);
        public void Update(Note note);
        public void Delete(int Id);

        public bool Exist(int Id);
    }
}
