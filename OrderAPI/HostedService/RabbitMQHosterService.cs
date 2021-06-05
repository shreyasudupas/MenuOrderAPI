using BuisnessLayer.AccessLayer.IAccessLayer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace OrderAPI
{
    public class RabbitMQHosterService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ISubsciber sub;

        public RabbitMQHosterService(ILoggerFactory loggerFactory,ISubsciber subsciber)
        {
            this._logger = loggerFactory.CreateLogger<RabbitMQHosterService>();
            sub = subsciber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Service Started....."); ;

                //call subscirber 
                //sub.GetBreakfastSubscriber();

                _logger.LogInformation("Service Ended....."); ;

                await Task.Delay(10000,stoppingToken);
            }
        }
    }
}
