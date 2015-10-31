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
        public Article Create(Article article)
        {
            if(db.Count(article.Slug) == 0)
            {
                return db.Create(article);
            }
            return null;
        }

        [HttpPut]
        public void Update(Article article)
        {
            if(db.Exist(article.id) && db.Count(article.Slug) <= 1)
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
