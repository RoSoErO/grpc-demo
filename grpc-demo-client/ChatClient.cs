using demo;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace grpc_demo_client
{
    public class ChatClient
    {
        private bool runBackgroundThread = true;
        private string name;

        public async Task start()
        {
            Console.WriteLine("What is your name:");
            name = Console.ReadLine();

            string message = "";

            using var channel = GrpcChannel.ForAddress("https://localhost:50001");
            var client = new Demo.DemoClient(channel);

            Console.WriteLine("Send a messages to the server! Type exit to stop");

            var reciver = new Thread(ReciveMessages);
            reciver.Start();

            while (true)
            { 
                message = Console.ReadLine();
                if (message == "exit") { break; }

                var reply = await client.SendMessageAsync(new Message() { Sender = name, MessageText = message });
            }

            runBackgroundThread = false;

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private async void ReciveMessages()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:50001");
            var client = new Demo.DemoClient(channel);

            int highestMessageId = -1;

            while (runBackgroundThread)
            {
                var messages = await client.GetMessagesAsync(new Query() { LatestId = highestMessageId });

                foreach (var message_ in messages.Messages_)
                {
                    if (message_.Id > highestMessageId)
                    {
                        highestMessageId = message_.Id;
                    }
                    // Do not show messages you sent yourself
                    if (message_.Sender != this.name) 
                    {
                        Console.WriteLine(message_.Sender + ": " + message_.MessageText);
                    }                    
                }
                Thread.Sleep(5000);
            }
        }
    }
}
