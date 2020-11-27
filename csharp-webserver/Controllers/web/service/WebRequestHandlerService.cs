using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Diagnostics;

using System.Reflection;
using System.Net.Http;

namespace csharp_webserver
{
    public class WebRequestHandlerService
    {
        private string _response;
        private Type _type;
        private HttpRequestType httpRequestType;
        private List<KeyValuePair<string, string>> _parameters;
        private string _content;


        public WebRequestHandlerService()
        {
            this._response = null;
            this.httpRequestType = HttpRequestType.GET;
            this._parameters = new List<KeyValuePair<string, string>>();
        }

        public WebRequestHandlerService handleFor(Type type)
        {

            this._type = type;
            return this;
        }

        public WebRequestHandlerService addParameter(string key, string value)
        {

            this._parameters.Add(
                new KeyValuePair<string, string>(key, value));
            return this;
        }

        public WebRequestHandlerService setContent(string c)
        {

            this._content = c;
            return this;
        }

        public WebRequestHandlerService setHttpType(HttpRequestType type)
        {

            this.httpRequestType = type;
            return this;
        }

        public WebRequestHandlerService handleRequest()
        {
            string handlerName = 
                $"csharp_webserver." +
                $"{this._type.Name}" +
                $"ResponseBuilder";

            try
            {

                Type handler = Type.GetType(handlerName);
                var responseBuilder = 
                        Activator.CreateInstance(handler) 
                            as AbstractResponseBuilder;

                this._response = responseBuilder
                    .setHttpType(httpRequestType)
                    .setParameters(_parameters)
                    .setContent(_content)
                        .getResponse();
            }
            catch (Exception e)
            {

                Global.logger.addLog(ServerLogEntry.builder()
                   .setLogLevel(LogLevel.ERROR)
                   .setMessage($"Exception occoured while processing response: {e.Message}"));

                this._response = "";
            }

            return this;
        }

        public HttpResponseMessage buildResponse()
        {

            HttpResponseMessage response = 
                new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            if (string.IsNullOrEmpty(this._response))
            {
                response.StatusCode = 
                    System.Net.HttpStatusCode.BadRequest;
            }
            else
            {
                response.Content =
                    new StringContent(
                        this._response,
                        System.Text.Encoding.UTF8, "application/text");
            }

            return response;
        }

   
    }
}