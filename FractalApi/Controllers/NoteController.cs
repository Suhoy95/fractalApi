using Domain.Abstract;
using Domain.Entities;
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
            return db.Create(note);
        }

        [HttpPut]
        public void Update(Item note)
        {
            if (db.Exist(note.id))
            {
                db.Update(note);
            }
        }

        // DELETE api/note/5
        public void Delete(int id)
        {
            if(db.Exist(id))
            {
                db.Delete(id);
            }
        }
    }
}
