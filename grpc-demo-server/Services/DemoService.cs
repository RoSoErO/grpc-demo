using demo;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_demo_server
{
    public class DemoService : Demo.DemoBase
    {
        private MessageService messageService;

        public DemoService(MessageService messageService)
        {
            this.messageService = messageService;
        }


        public override Task<StatusReply> SendMessage(Message message, ServerCallContext context)
        {
            var response = new StatusReply() { StatusText = "Message ok! Message length: " + message.MessageText.Length };

            messageService.AddMessage(message);

            return Task.FromResult(response);
        }

        public override Task<Messages> GetMessages(Query query, ServerCallContext context)
        {
            var response = new Messages();
            response.Messages_.AddRange(messageService.GetAllMessages().Select(e => new Message() { MessageText = e.MessageText }));

            return Task.FromResult(response);
        }
    }
}
