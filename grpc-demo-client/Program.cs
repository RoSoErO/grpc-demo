using System;
using System.Threading.Tasks;
using demo;
using Grpc.Net.Client;

namespace grpc_demo_client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chatClient = new ChatClient();

            await chatClient.start();

            //string message = "";

            //using var channel = GrpcChannel.ForAddress("https://localhost:50001");
            //var client = new Demo.DemoClient(channel);

            //while (true)
            //{
            //    Console.WriteLine("Send a message to the server: (exit to stop)");
            //    message = Console.ReadLine();
            //    if (message == "exit") { break; }

            //    var reply = await client.SendMessageAsync(new Message() { MessageText = message });
            //    Console.WriteLine(reply.StatusText);
            //}


            //Console.WriteLine("All messages:");
            //var messages = await client.GetMessagesAsync(new Query());
            //foreach (var message_ in messages.Messages_)
            //{
            //    Console.WriteLine(message_.MessageText);
            //}

            //Console.WriteLine("Press enter to exit");
            //Console.ReadLine();
        }
    }
}
