using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FractalApi.HttpExceptions
{
    public class HttpExceptionFactory
    {
        public static HttpResponseException BadSlug()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("BadSlug")
            };

            return new HttpResponseException(response);
        }

        public static HttpResponseException InvalidModel()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid Model")
            };

            return new HttpResponseException(response);
        }
    }
}