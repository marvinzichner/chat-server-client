using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class ChatControllerResponseBuilder : AbstractResponseBuilder
    {

        private string response;

        public ChatControllerResponseBuilder():base()
        {

        }

        private void handle()
        {
            if (this.isGetRequest())
            {
                Global.logger.addLog(ServerLogEntry.builder()
                   .setLogLevel(LogLevel.INFO)
                   .setMessage("Handling name discovery request from remote client"));

                response = 
                    "{\"name\": \"" + Global.messageStoreService.getClientName() + "\"}";
            }

            if (this.isPostRequest())
            {
                try
                {
                    Global.logger.addLog(ServerLogEntry.builder()
                       .setLogLevel(LogLevel.DEBUG)
                       .setMessage("Received remote message. Parsing message to object."));

                    Message message = 
                        JsonConvert.DeserializeObject<Message>(
                            this.getContent());

                    Global.messageStoreService.addMessage(message);

                    response = $"ACCEPTED";

                }
                catch (Exception e)
                {
                    Global.logger.addLog(ServerLogEntry.builder()
                       .setLogLevel(LogLevel.ERROR)
                       .setMessage($"Processing of message failed with exception: {e.Message}"));

                    response = $"FAILED;{e.Message}";
                }
            }
        }

        public override string getResponse()
        {

            handle();
            return response;
        }
    }
}