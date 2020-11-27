
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace csharp_webserver
{
    public class RefreshController : ApiController
    {
        private WebRequestHandlerService webRequestHandlerService;

        public RefreshController()
        {
            webRequestHandlerService = new WebRequestHandlerService();
        }

        // GET: api/Refresh
        public HttpResponseMessage Get()
        {

            return webRequestHandlerService
                .handleFor(this.GetType())
                .setHttpType(HttpRequestType.GET)
                    .handleRequest()
                    .buildResponse();
        }

        // GET: api/Refresh/5
        public HttpResponseMessage Get(int id)
        {

            return webRequestHandlerService
                .handleFor(this.GetType())
                .setHttpType(HttpRequestType.GET)
                .addParameter("id", id.ToString())
                    .handleRequest()
                    .buildResponse();
        }



        // POST: api/Refresh
        public HttpResponseMessage Post([FromBody] string value)
        {

            return webRequestHandlerService
                .handleFor(this.GetType())
                .setHttpType(HttpRequestType.POST)
                .setContent(value)
                    .handleRequest()
                    .buildResponse();
        }

        // PUT: api/Refresh/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Refresh/5
        public void Delete(int id)
        {
        }
    }
}
