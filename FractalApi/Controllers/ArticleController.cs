using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace FractalApi.Controllers
{
    public class ArticleController : ApiController
    {
        private IArticleRepository db;

        public ArticleController(IArticleRepository db)
        {
            this.db = db;
        }
        
        public Article Get(string slug)
        {
            return db.Get(slug);
        }

        [HttpPost]
        public void Create(Article article)
        {
            if(db.Count(article.Slug) == 0)
            {
                db.Create(article);
            }
        }

        [HttpPut]
        public void Update(Article article)
        {
            if(db.Exist(article.Id) && db.Count(article.Slug) <= 1)
            {
                db.Update(article);
            }
        }

        public void Delete(int id)
        {
            if(db.Exist(id))
            {
                db.Delete(id);
            }
        }
    }
}
