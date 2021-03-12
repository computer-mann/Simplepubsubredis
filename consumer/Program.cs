using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {

            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

                ISubscriber sub = redis.GetSubscriber();
                var queue=await sub.SubscribeAsync("console-message");
                //while (true)
                //{
                //    Console.WriteLine((await queue.ReadAsync()).Message);

                //}
                //while(queue.wa)



            }
            catch (RedisConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}
