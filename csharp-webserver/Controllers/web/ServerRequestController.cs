using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace csharp_webserver
{
    public class ServerRequestController
    {

        private HttpClientExecutionService httpClientExecutionService;

        public ServerRequestController()
        {
            httpClientExecutionService = new HttpClientExecutionService();
        }

        public string doGetRequest(string url)
        {

            return httpClientExecutionService
                .newRequest()
                .setUrl(url)
                .setRequestType(HttpRequestType.GET)
                    .doRequest()
                    .getResponse();
        }

        public string doPostRequest(string url, string content)
        {

            return httpClientExecutionService
                .newRequest()
                .setUrl(url)
                .setRequestType(HttpRequestType.POST)
                .setContent(content)
                    .doRequest()
                    .getResponse();
        }

        public string doPutRequest(string url, string content)
        {

            return httpClientExecutionService
                .newRequest()
                .setUrl(url)
                .setContent(content)
                .setRequestType(HttpRequestType.PUT)
                    .doRequest()
                    .getResponse();
        }

        public HttpStatusCode sendMessage(string recipient, string msg)
        {

            Message message = Message.builder()
                .setContent(msg)
                .setFromHost($"http://{HttpContext.Current.Request.Url.Host}:{HttpContext.Current.Request.Url.Port}")
                .setOrigin($"{HttpContext.Current.Request.Url.Host} ({HttpContext.Current.Request.Url.Port})");

            var json = JsonConvert.SerializeObject(message);
            HttpStatusCode response = httpClientExecutionService
                .newRequest()
                .setUrl(recipient)
                .setContent(json)
                .setRequestType(HttpRequestType.POST)
                    .doRequest()
                    .getResponseCode();

            if (response == HttpStatusCode.OK)
            {
                message
                    .setMySelf(true)
                    .setDelivered(true);

                Global.messageStoreService.addMessage(message);
            } 
            else
            {
                message
                    .setMySelf(true)
                    .setDelivered(false);

                Global.logger.addLog(ServerLogEntry.builder()
                    .setLogLevel(LogLevel.ERROR)
                    .setMessage($"Response failed with code={response.ToString()}"));

                Global.messageStoreService.addMessage(message);
            }

            return response;
        }
    }
}