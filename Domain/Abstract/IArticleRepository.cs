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
        public Article Get(int Id);
        public Article Create(Article article);
        public void Update(Article article);
        public void Delete(int Id);

        public bool Exist(int Id);
    }
}
