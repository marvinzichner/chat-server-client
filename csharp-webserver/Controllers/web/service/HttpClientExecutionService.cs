using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;

namespace csharp_webserver
{
    public class HttpClientExecutionService
    {

        private List<KeyValuePair<string, string>> parameters;
        private HttpRequestType type;
        private string responsePlain;
        private HttpClient httpClient;
        private HttpStatusCode responseCode;
        private string content;
        private int requestCount;
        private bool failed;
        private string url;

        public HttpClientExecutionService setRequestType(HttpRequestType t) { this.type = t; return this; }
        public HttpClientExecutionService setUrl(string s) { this.url = s; return this; }
        public string getUrl() { return url; }

        public HttpClientExecutionService()
        {

            this.parameters = new List<KeyValuePair<string, string>>();
            this.type = HttpRequestType.GET;
            this.responsePlain = "";
            this.requestCount = 0;
            this.failed = false;
            this.httpClient = null;
            this.url = "";
        }

        public HttpClientExecutionService newRequest()
        {

            this.parameters = new List<KeyValuePair<string, string>>();
            this.type = HttpRequestType.GET;
            this.responsePlain = "";
            this.failed = false;
            this.httpClient = null;
            this.url = "";

            return this;
        }

        public HttpClientExecutionService addParameter(string key, string value)
        {

            this.parameters.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public HttpClientExecutionService setContent(string c)
        {

            this.content = c;
            return this;
        }

        private void buildUrlWithParams()
        {

            if (!this.url.Equals("") && parameters.Count > 0)
            {

                if (!this.url.StartsWith("http://")) 
                    this.url = $"http://{this.url}";

                int count = 0;
                this.url = $"{this.url}?";
                this.parameters.ForEach(parameter =>
                {
                    if (count == 0)
                    {
                        this.url = $"{this.url}{parameter.Key}={parameter.Value}";
                    }
                    else
                    {
                        this.url = $"{this.url}&{parameter.Key}={parameter.Value}";
                    }

                    count++;
                });

            }

            this.url = this.url.Replace(" ", "%20");
        }

        public HttpClientExecutionService doRequest()
        {

            buildUrlWithParams();
            this.requestCount++;

            this.content = Precondition
                .setDefaultIfStringIsNullOrEmpty(this.content, "");

            try
            {

                Global.logger.addLog(ServerLogEntry.builder()
                   .setLogLevel(LogLevel.DEBUG)
                   .setMessage($"Starting WebRequest; url={url}, content={content}"));

               
                this.httpClient = new HttpClient();
                this.httpClient.BaseAddress = new Uri(this.url);

                var response =
                    this.httpClient.PostAsJsonAsync(
                        "api/chat", this.content).Result;

                this.responsePlain = response.Content.ToString();
                this.responseCode = response.StatusCode;
                
                Global.logger.addLog(ServerLogEntry.builder()
                   .setLogLevel(LogLevel.DEBUG)
                   .setMessage($"Executed request (url={httpClient.BaseAddress.ToString()}, server={response.Headers.Server}) with response_code={responseCode.ToString()}"));

                /*
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(this.url));
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                request.ContentLength = byteArray.Length;
                request.Method = type.ToString();
                request.ContentType = "application/json";

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                */

            }
            catch (Exception e)
            {
                
                Global.logger.addLog(ServerLogEntry.builder()
                   .setLogLevel(LogLevel.ERROR)
                   .setMessage($"Request failed with exception: {e.Message}"));

                failed = true;
            }

            return this;
        }

        public HttpStatusCode getResponseCode()
        {
            return responseCode;
        }

        public string getResponse()
        {
            if (!failed)
                return this.responsePlain;

            return $"";
        }
    }
}