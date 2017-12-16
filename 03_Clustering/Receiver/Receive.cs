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
                    var host = x.Host(new Uri("rabbitmq://mainHostName"),
                        h =>
                        {
                            h.Username("rabbitdemo"); 
                            h.Password("demorabbit");
                            h.UseCluster(cluster =>
                            {
                                string[] hostnames = "mainHostName.office.sbs;slaveClusterHostName.office.sbs".Split(';');
                                cluster.ClusterMembers = hostnames;
                            });
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

        //private static IBusControl CreateBusReceiveEndpoint()
        //{
        //    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        //    {
        //        var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
        //        {
        //            h.Username("guest");
        //            h.Password("guest");
        //        });

        //        cfg.ReceiveEndpoint(host, "Contracts.Account",
        //            e =>
        //            {
        //                e.ExchangeType = ExchangeType.Fanout;
        //                e.Consumer(typeof (Contracts.IAccount), myType => typeof (AccountConsumer));
        //            });
        //    });

        //    return busControl;
        //}
    }
}