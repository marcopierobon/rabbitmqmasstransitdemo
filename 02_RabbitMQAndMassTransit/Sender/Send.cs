using System;
using System.Threading.Tasks;
using MassTransit;

namespace Sender
{
    public class Send
    {
        public static void Main()
        {
            var busSendEndpoint = CreateBusSendEndpoint();
            busSendEndpoint.Start();

            var newAccount = new Contracts.Account()
            {
                AccountName = "RabbitMQDemo Inc",
                NumberOfEmployees = 45,
                YearOfAccounting = 2017,
                YearOfEstablishment = 2017
            };

            Console.WriteLine(" Press [enter] to send.");
            Console.ReadLine();
            var result = SendNewAccountMessage(busSendEndpoint, newAccount);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            busSendEndpoint.Stop();
        }

        private static IBusControl CreateBusSendEndpoint()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("rabbitdemo");
                    h.Password("demorabbit");
                });
            });

            return busControl;
        }

        public static async Task SendNewAccountMessage(IBusControl publishEndpoint, Contracts.IAccount newAccount)
        {
            var destinationAddress = new Uri("rabbitmq://localhost/consumer_queue");
            var destination = await publishEndpoint.GetSendEndpoint(destinationAddress);

            await destination.Send<Contracts.IAccount>(newAccount);
        }
    }
}