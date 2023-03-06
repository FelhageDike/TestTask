using Microsoft.EntityFrameworkCore;
using SearchApi.DbModel;
using SearchApi.Interface;

namespace SearchApi
{
    public class BackgroundServices : BackgroundService
    {
        private readonly IMessageProducer _messageProduser;

        public BackgroundServices(IMessageProducer messageProduser)
        {
            _messageProduser = messageProduser;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageProduser.ReceiveMessageProduct();

            }
             return Task.CompletedTask;
        }
    }
}
