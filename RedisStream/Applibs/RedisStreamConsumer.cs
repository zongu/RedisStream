
namespace RedisStream.Applibs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using RedisStream.Model;

    /// <summary>
    /// Redis stream 消費者
    /// </summary>
    public class RedisStreamConsumer
    {
        private IEnumerable<string> topics;

        private IPubSubDispatcher<RedisStreamEventStream> dispatcher;

        /// <summary>
        /// 查找起始位置
        /// </summary>
        private string postion = "0-0";

        public RedisStreamConsumer(IEnumerable<string> topics, IPubSubDispatcher<RedisStreamEventStream> dispatcher)
        {
            this.topics = topics;
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
                Task.Run(() =>
                {
                    RedisStreamFactory.UseConnection(redis =>
                    {
                        while (RedisStreamFactory.IsWork)
                        {
                            var msgs = redis.StreamRead(topic, postion, 1);

                            if (msgs.Any())
                            {
                                // 複寫當前取得最新的位置
                                this.postion = msgs.First().Id.ToString();

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
