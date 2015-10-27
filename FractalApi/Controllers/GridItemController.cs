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
    public class GridItemController : ApiController
    {
        private IGridRepository db;

        public GridItemController(IGridRepository db)
        {
            this.db = db;
        }

        [HttpPost]
        public GridItem Create(GridItem grid)
        {
            return db.Create(grid);
        }

        [HttpPut]
        public void Update(GridItem grid)
        {
            if(db.Exsist(grid.Id))
            {
                db.Update(grid);
            }
        }
    }
}
