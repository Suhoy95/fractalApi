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
    public class GridItemController : ApiController
    {
        private IGridRepository db;
        private IUserRepository userDb;

        public GridItemController(IGridRepository db, IUserRepository userDb)
        {
            this.db = db;
            this.userDb = userDb;
        }

        [HttpPost]
        public Item Create(Item grid)
        {
            CheckGridItem(grid);

            return db.Create(grid);
        }

        [HttpPut]
        public void Update(Item grid)
        {
            CheckGridItem(grid);

            if(db.Exsist(grid.id))
            {
                db.Update(grid);
            }
        }

        private void CheckGridItem(Item grid)
        {
            if (!userDb.HasPermission(User.Identity.Name, grid.gridId))
                throw HttpExceptionFactory.Forbidden();

            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();

            if (!db.IsCorrectSlug(grid.slug, grid.id))
                throw HttpExceptionFactory.BadSlug();
        }
    }
}
