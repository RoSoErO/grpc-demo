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

        public async Task start()
        {
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

                var reply = await client.SendMessageAsync(new Message() { MessageText = message });
                //Console.WriteLine(reply.StatusText);
            }

            runBackgroundThread = false;

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private async void ReciveMessages()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:50001");
            var client = new Demo.DemoClient(channel);

            while (runBackgroundThread)
            {
                Console.WriteLine("All messages:");
                var messages = await client.GetMessagesAsync(new Query());
                foreach (var message_ in messages.Messages_)
                {
                    Console.WriteLine(message_.MessageText);
                }

                Thread.Sleep(5000);
            }
        }
    }
}
