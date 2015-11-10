using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Abstract;

namespace FractalApi.Controllers
{
    public class CoordController : ApiController
    {
        private IItemRepository db;

        public CoordController(IItemRepository db)
        {
            this.db = db;
        }

        [HttpPut]
        public void Update(int[][] grid)
        {
            db.UpdateCoord(grid);
        }
    }
}
