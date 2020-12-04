using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class MessageStoreService
    {

        private List<Message> messages;
        private List<Action> subscribers;
        private string clientName;
        private bool changed;

        public List<Message> getMessages() { return messages; }
        public MessageStoreService setClientName(string s) { this.clientName = s; return this; }
        public string getClientName() { return clientName; }
        public int countMessages() { return this.messages.Count(); }

        public MessageStoreService()
        {
            messages = new List<Message>();
            subscribers = new List<Action>();
            changed = false;
            clientName = "Anonymous";
        }

        public MessageStoreService subscribe(Action a)
        {
            bool isPresent = false;
            this.subscribers.ForEach(action =>
            {
                if (a.Equals(action)) isPresent = true;
            });

            if (!isPresent)
            {
                this.subscribers.Add(a);
            }

            return this;
        }

        private void notify()
        {
            changed = true; 

            this.subscribers.ForEach(a =>
            {
                // a.Invoke();
            });
        }

        public List<string> getOrigins()
        {

            List<string> origins = new List<string>();

            this.messages
                .FindAll(message => message.delivered)
                .ForEach(message =>
            {
                if (origins.IndexOf(message.getOrigin()) == -1) {

                    if (origins.FindAll(
                        value => value == message.getHost()).Count == 0)
                            origins.Add(message.getHost());
                }
            });

            return origins;
        }

        public string changedSinceLastRequest()
        {
            string response = "{ \"event\": \"NO_CHANGE\"}";

            if (changed)
            {
                changed = false;
                response = "{ \"event\": \"CHANGED\"}";
            }

            return response;
        }

        public MessageStoreService addMessage(Message m)
        {
            this.messages.Add(m);
            this.notify();

            Global.logger.addLog(ServerLogEntry.builder()
                .setLogLevel(LogLevel.INFO)
                .setMessage($"Stored new message from {m.getOrigin()}"));

            return this;
        }
    }
}