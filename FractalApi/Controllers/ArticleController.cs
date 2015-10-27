using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FractalApi.Controllers
{
    public class ArticleController : ApiController
    {
        // GET api/article
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/article/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/article
        public void Post([FromBody]string value)
        {
        }

        // PUT api/article/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/article/5
        public void Delete(int id)
        {
        }
    }
}
