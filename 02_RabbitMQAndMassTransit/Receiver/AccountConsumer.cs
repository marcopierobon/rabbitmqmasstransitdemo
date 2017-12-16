using System.Threading.Tasks;
using MassTransit;
using System;

namespace Receiver
{
    public class AccountConsumer : IConsumer<Contracts.IAccount>
    {
        public Task Consume(ConsumeContext<Contracts.IAccount> context)
        {
            var message = context.Message;
            Console.Out.WriteLineAsync(string.Format(" [x] Received new Account in a message with id {0}", context.MessageId));
            Console.Out.WriteLineAsync(string.Format(" [x] AccountName {0}", message.AccountName));
            Console.Out.WriteLineAsync(string.Format(" [x] YearOfAccounting {0}", message.YearOfAccounting));
            Console.Out.WriteLineAsync(string.Format(" [x] YearOfEstablishment {0}", message.YearOfEstablishment));
            Console.Out.WriteLineAsync(string.Format(" [x] NumberOfEmployees {0}", message.NumberOfEmployees));

            return Task.CompletedTask;
        }
    }
}
