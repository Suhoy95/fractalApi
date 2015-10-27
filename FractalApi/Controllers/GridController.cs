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
    public class GridController : ApiController
    {
        private IGridRepository db;

        public GridController(IGridRepository db)
        {
            this.db = db;
        }

        public Grid Get(string slug)
        {
            return db.GetGrid(slug);
        }

        public bool Delete(string slug)
        {
            if(db.Exsist(slug))
            {
                return db.DeleteGrid(slug);
            }
            return false;
        }
    }
}
