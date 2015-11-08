﻿using Domain.Abstract;
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
        public Item Create(Item grid)
        {
            if (grid.slug == "sad")
                BadSlug();

            grid.id = 10;
            return grid;// db.Create(grid);
        }

        [HttpPut]
        public void Update(Item grid)
        {
            if(db.Exsist(grid.id))
            {
                db.Update(grid);
            }
        }

        private void BadSlug()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("BadSlug")
            };
            throw new HttpResponseException(response);
        }
    }
}
