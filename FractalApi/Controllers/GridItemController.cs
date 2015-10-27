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
        // GET api/griditem
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/griditem/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/griditem
        public void Post([FromBody]string value)
        {
        }

        // PUT api/griditem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/griditem/5
        public void Delete(int id)
        {
        }
    }
}
