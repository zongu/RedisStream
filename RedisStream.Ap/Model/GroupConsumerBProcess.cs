
namespace RedisStream.Ap.Model
{
    using System;
    using System.Threading.Tasks;
    using Autofac;
    using RedisStream.Applibs;
    using RedisStream.Model;

    public class GroupConsumerBProcess : IProcess
    {
        private string[] subscribeTopics = new string[] { "ProducerB" };

        private string topic = "GroupConsumerB";

        public Task Execute()
        {
            return Task.Run(() =>
            {
                try
                {
                    var consumer = new RedisStreamGroupConsumer(
                        this.subscribeTopics,
                        topic,
                        Applibs.AutofacConfig.Container.Resolve<IPubSubDispatcher<RedisStreamEventStream>>());

                    consumer.Regist();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
