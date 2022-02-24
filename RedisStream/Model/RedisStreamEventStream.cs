
namespace RedisStream.Model
{
    /// <summary>
    /// Redis stream 事件流
    /// </summary>
    public class RedisStreamEventStream : EventStream
    {
        public RedisStreamEventStream(string type, string data, long utcTimeStamp)
        {
            Type = type;
            Data = data;
            UtcTimeStamp = utcTimeStamp;
        }
    }

    /// <summary>
    /// RedisStream處裡事件介面
    /// </summary>
    public interface IRedisStreamHandler : IPubSubHandler<RedisStreamEventStream>
    {
    }
}
