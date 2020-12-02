using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace csharp_webserver.Controllers
{
    public class ChatController : ApiController
    {
    
        private WebRequestHandlerService webRequestHandlerService;

        public ChatController()
        {
            webRequestHandlerService = new WebRequestHandlerService();
        }

        // GET: api/ChatMessage
        public HttpResponseMessage Get()
        {

            Global.logger.addLog(ServerLogEntry.builder()
                .setLogLevel(LogLevel.DEBUG)
                .setMessage("request for: GET request api/Chat"));

            return webRequestHandlerService
               .handleFor(this.GetType())
               .setHttpType(HttpRequestType.GET)
                   .handleRequest()
                   .buildResponse();
        }

        // POST: api/ChatMessage
        public HttpResponseMessage Post([FromBody]string value)
        {

            Global.logger.addLog(ServerLogEntry.builder()
                .setLogLevel(LogLevel.DEBUG)
                .setMessage($"request for: POST request api/Chat; content={value}"));

            return webRequestHandlerService
               .handleFor(this.GetType())
               .setHttpType(HttpRequestType.POST)
               .setContent(value)
                   .handleRequest()
                   .buildResponse();
        }

        // TODO: fix client request value not mapped
        public HttpResponseMessage Post(string method, [FromBody] string value)
        {

            return new HttpRequestMessage().CreateResponse("BLA");
        }

        // PUT: api/ChatMessage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChatMessage/5
        public void Delete(int id)
        {
        }
    }
}
