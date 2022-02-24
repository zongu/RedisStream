
namespace RedisStream.Applibs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using RedisStream.Model;
    using StackExchange.Redis;

    /// <summary>
    /// Redis stream Group消費者
    /// </summary>
    public class RedisStreamGroupConsumer
    {
        private IEnumerable<string> topics;

        private string currentTopic;

        private IPubSubDispatcher<RedisStreamEventStream> dispatcher;

        public RedisStreamGroupConsumer(IEnumerable<string> topics, string currentTopic, IPubSubDispatcher<RedisStreamEventStream> dispatcher)
        {
            this.topics = topics;
            this.currentTopic = currentTopic;
            this.dispatcher = dispatcher;
        }

        public void Regist()
        {
            if (!RedisStreamFactory.IsWork)
            {
                throw new Exception("Please execute RedisStreamFactory.Start first");
            }

            this.topics.ToList().ForEach(topic =>
            {
                RedisStreamFactory.UseConnection(redis =>
                {
                    try
                    {
                        redis.StreamCreateConsumerGroup(topic, $"{this.currentTopic}-{topic}", StreamPosition.NewMessages);
                    }
                    catch (RedisException ex)
                    {
                        if (ex.Message != "BUSYGROUP Consumer Group name already exists")
                        {
                            throw ex;
                        }
                    }

                    return false;
                });

                Task.Run(() =>
                {
                    RedisStreamFactory.UseConnection(redis =>
                    {
                        while (RedisStreamFactory.IsWork)
                        {
                            var msgs = redis.StreamReadGroup(topic, $"{this.currentTopic}-{topic}", this.currentTopic, ">", count: 1, noAck: true);

                            if (msgs.Any())
                            {
                                this.dispatcher.DispatchMessage(new RedisStreamEventStream(
                                    msgs.First().Values.First().Name.ToString(),
                                    msgs.First().Values.First().Value.ToString(),
                                    long.Parse(msgs.First().Id.ToString().Split('-')[0])));
                            }

                            SpinWait.SpinUntil(() => false, 50);
                        }

                        return false;
                    });
                });
            });
        }
    }
}
