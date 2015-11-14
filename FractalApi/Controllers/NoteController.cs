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
    [Authorize]
    public class NoteController : ApiController
    {
        private INoteRepository db;
        private IUserRepository userDb;

        public NoteController(INoteRepository db, IUserRepository userDb)
        {
            this.db = db;
            this.userDb = userDb;
        }

        [HttpPost]
        public Item Create(Item note)
        {
            CheckNote(note);
            
            return db.Create(note);           
        }        

        [HttpPut]
        public void Update(Item note)
        {
            CheckNote(note);

            if (db.Exist(note.id))
            {
                db.Update(note);
            }
        }

        [HttpDelete]
        public void Delete(Item note)
        {
            CheckNote(note);

            if(db.Exist(note.id))
            {
                db.Delete(note.id);
            }
        }

        private void CheckNote(Item note)
        {
            if (!userDb.HasPermission(User.Identity.Name, note.gridId))
                throw HttpExceptionFactory.Forbidden();

            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();
        }
    }
}
