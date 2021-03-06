﻿using Domain.Abstract;
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
    public class GridController : ApiController
    {
        private IGridRepository db;
        private IPermissionChecker userDb;

        public GridController(IGridRepository db, IPermissionChecker userDb)
        {
            this.db = db;
            this.userDb = userDb;
        }

        [AllowAnonymous]
        public Grid Get(string slug)
        {
            try
            {
                var grid = db.Get(slug);
                if(User.Identity.IsAuthenticated){
                    grid.HasPermission = userDb.GridAllowed(User.Identity.Name, grid.Id);
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
            if (!userDb.GridAllowed(User.Identity.Name, grid.Id))
                throw HttpExceptionFactory.Forbidden();

            if (!ModelState.IsValid)
                throw HttpExceptionFactory.InvalidModel();

            if (!db.IsCorrectSlug(grid.Slug, grid.Id))
                throw HttpExceptionFactory.BadSlug();

            if(db.Exsist(grid.Id))
            {
                db.Update(grid);
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            if(!userDb.GridAllowed(User.Identity.Name, id))
                throw HttpExceptionFactory.Forbidden();

            if(db.Exsist(id))
            {
                db.Delete(id);
            }
        }
    }
}
