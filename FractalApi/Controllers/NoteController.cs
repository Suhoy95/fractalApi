using Domain.Abstract;
using Domain.Entities;
using FractalApi.HttpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FractalApi.Controllers
{
    public class NoteController : ApiController
    {
        private INoteRepository db;

        public NoteController(INoteRepository db)
        {
            this.db = db;
        }

        [HttpPost]
        public Item Create(Item note)
        {          
            if(ModelState.IsValid)
                return db.Create(note);

            throw HttpExceptionFactory.InvalidModel();
        }

        [HttpPut]
        public void Update(Item note)
        {
            if(!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();

            if (db.Exist(note.id))
            {
                db.Update(note);
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            if(db.Exist(id))
            {
                db.Delete(id);
            }
        }
    }
}
