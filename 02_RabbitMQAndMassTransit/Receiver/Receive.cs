//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.RegularExpressions;
using GreenPipes;
using MassTransit;
using RabbitMQ.Client;

namespace Receiver
{
    class Receive
    {
        public static void Main()
        {
            var busReceiveEndpoint = CreateBusReceiveEndpoint();
            busReceiveEndpoint.Start();
            Console.WriteLine(" Consumer started");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            busReceiveEndpoint.Stop();
        }

        public static IBusControl CreateBusReceiveEndpoint()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x => 
                { 
                    var host = x.Host(new Uri("rabbitmq://localhost"), 
                        h => {

                            h.Username("rabbitdemo");
                            h.Password("demorabbit");
                        }); // declare the receive endpoint on the host 
                        x.ReceiveEndpoint(host, "consumer_queue",
                        e =>
                        {
                            // configure the consumer using the default constructor // consumer factory. 
                            e.Consumer<AccountConsumer>();
                        });
                });
            return bus;
        }
    }
}