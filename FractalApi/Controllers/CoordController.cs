using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;
using FractalApi.HttpExceptions;

namespace FractalApi.Controllers
{
    [Authorize]
    public class CoordController : ApiController
    {
        private IItemRepository db;
        private IUserRepository userDb;

        public CoordController(IItemRepository db, IUserRepository userDb)
        {
            this.db = db;
            this.userDb = userDb;
        }

        [HttpPut]
        public void Update(Coords grid)
        {
            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();

            db.UpdateCoord(grid.coords);
        }
    }
}
