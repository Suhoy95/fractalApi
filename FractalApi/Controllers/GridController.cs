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
    public class GridController : ApiController
    {
        private IGridRepository db;
        private IUserRepository userDb;

        public GridController(IGridRepository db, IUserRepository userDb)
        {
            this.db = db;
            this.userDb = userDb;
        }

        public Grid Get(string slug)
        {
            try
            {
                var grid = db.Get(slug);
                if(User.Identity.IsAuthenticated){
                    grid.HasPermission = userDb.HasPermission(User.Identity.Name, grid.Id);
                }
                return grid;
            } catch(Exception ex)
            {
                if (ex.Message == "Grid Not Found")
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                throw ex;
            }
        }

        [HttpPut]
        public void Update(PartialGrid grid)
        {
            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();

            if (!db.IsCorrectSlug(grid.Slug, grid.Id))
                throw HttpExceptionFactory.BadSlug();

            if(db.Exsist(grid.Id))
            {
                db.Update(grid);
            }
        }

        public void Delete(int Id)
        {
            if(db.Exsist(Id))
            {
                db.Delete(Id);
            }
        }
    }
}
