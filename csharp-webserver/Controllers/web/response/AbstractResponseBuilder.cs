using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public abstract class AbstractResponseBuilder
    {
        private HttpRequestType httpRequestType;
        private List<KeyValuePair<string, string>> _parameters;
        private string _content;

        public AbstractResponseBuilder()
        {
            this._parameters = new List<KeyValuePair<string, string>>();
        }

        public string requestContent()
        {
            return this._content;
        }

        public bool isPostRequest()
        {
            return httpRequestType == HttpRequestType.POST;
        }

        public bool isGetRequest()
        {
            return httpRequestType == HttpRequestType.GET;
        }

        public bool isPutRequest()
        {
            return httpRequestType == HttpRequestType.PUT;
        }

        public List<KeyValuePair<string, string>> requestParameters()
        {
            return this._parameters;
        }

        public string getRequestParameter(string key)
        {
            string response = this.requestParameters().Find(
                pair => pair.Key == key).Value;

            if (string.IsNullOrEmpty(response))
            {
                response = "";
            }

            return response;
        }

        
        public abstract string getResponse();

        public AbstractResponseBuilder setHttpType(HttpRequestType type)
        {

            this.httpRequestType = type;
            return this;
        }

        public AbstractResponseBuilder setParameters
            (List<KeyValuePair<string, string>> parameters)
        {

            this._parameters = parameters;
            return this;
        }

        public string getContent()
        {
            return this._content;
        }

        public AbstractResponseBuilder setContent(string c)
        {

            this._content = 
                Precondition.setDefaultIfStringIsNullOrEmpty(c, "");
            return this;
        }
    }
}