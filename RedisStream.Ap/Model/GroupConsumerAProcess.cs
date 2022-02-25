
namespace RedisStream.Ap.Model
{
    using System;
    using System.Threading.Tasks;
    using Autofac;
    using RedisStream.Applibs;
    using RedisStream.Model;

    public class GroupConsumerAProcess : IProcess
    {
        private string[] subscribeTopics = new string[] { "ProducerA" };

        private string topic = "GroupConsumerA";

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
