using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class Message
    {

        public string content;
        public string origin;
        public string fromHost;
        public bool mySelf;
        public bool delivered;
        private DateTime created;

        public Message()
        {
            created = DateTime.Now;
        }

        public static Message builder() { return new Message(); }
        public Message setContent(string s) { this.content = s; return this; }
        public Message setFromHost(string s) { this.fromHost = s; return this; }
        public Message setOrigin(string s) { this.origin = s; return this; }
        public Message setMySelf(bool b) { this.mySelf = b; return this; }
        public Message setDelivered(bool b) { this.delivered = b; return this; }
        public string getContent() { return content; }
        public string getOrigin() { return origin; }
        public string getHost() { return fromHost; }
        public bool getDelivered() { return delivered; }

        public bool getMySelf() { return mySelf; }

        public String getCreatedTimestamp()
        {
            return $"{created.ToShortDateString()} {created.ToShortTimeString()}";
        }

    }
}