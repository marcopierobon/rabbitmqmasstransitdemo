using System;
using RabbitMQ.Client;
using System.Text;

namespace Sender 
{ 
    public class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                const string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                Console.WriteLine(" Press [enter] to send.");
                Console.ReadLine();
                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}