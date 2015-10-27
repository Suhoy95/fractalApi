using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    interface IArticleRepository
    {
        Article Get(int Id);
        Article Create(Article article);
        void Update(Article article);
        void Delete(int Id);

        bool Exist(int Id);
    }
}
