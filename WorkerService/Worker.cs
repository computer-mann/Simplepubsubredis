using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

                ISubscriber sub = redis.GetSubscriber();
                var queue = await sub.SubscribeAsync("console-message");
                //while (true)
                //{
                //    Console.WriteLine((await queue.ReadAsync()).Message);

                //}
                //while(queue.wa)
                while (!stoppingToken.IsCancellationRequested)
                {
                    queue.OnMessage(message =>
                    {
                        _logger.LogInformation("The message is {message}", message.Message);

                    });
                    //  await Task.Delay(1000, stoppingToken);
                }


            }
            catch (RedisConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
