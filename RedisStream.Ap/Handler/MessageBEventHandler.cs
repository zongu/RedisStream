
namespace RedisStream.Ap.Handler
{
    using System;
    using RedisStream.Model;

    public class MessageBEventHandler : IRedisStreamHandler
    {
        public bool Handle(RedisStreamEventStream stream)
        {
            try
            {
                Console.WriteLine($"Type:{stream.Type} Data:{stream.Data} TS:{stream.UtcTimeStamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
    }
}
