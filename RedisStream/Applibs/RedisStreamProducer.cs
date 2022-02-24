
namespace RedisStream.Applibs
{
    using Newtonsoft.Json;

    /// <summary>
    /// Redis stream 生產者
    /// </summary>
    public static class RedisStreamProducer
    {
        /// <summary>
        /// 推送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topic">topic</param>
        /// <param name="data">事件</param>
        /// <param name="maxLength">Steam留存筆數</param>
        /// <returns>StreamId</returns>
        public static string Publish<T>(string topic, T data, int maxLength = 1000)
        {
            return RedisStreamFactory.UseConnection(redis =>
            {
                return redis.StreamAdd(topic, data.GetType().Name, JsonConvert.SerializeObject(data), maxLength: maxLength, useApproximateMaxLength: true);
            });
        }
    }
}
