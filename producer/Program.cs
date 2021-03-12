using StackExchange.Redis;
using System;

namespace producer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
             ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
                
                ISubscriber sub = redis.GetSubscriber();
                string end = "",input="a";
                while (!string.Equals(end, input))
                {
                    input = Console.ReadLine();
                    sub.Publish("console-message", input);
                }

            }catch(RedisConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}
