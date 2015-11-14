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
        private IPermissionChecker userDb;

        public NoteController(INoteRepository db, IPermissionChecker userDb)
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
            if (!userDb.ItemAllowed(User.Identity.Name, note))
                throw HttpExceptionFactory.Forbidden();

            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();
        }
    }
}
