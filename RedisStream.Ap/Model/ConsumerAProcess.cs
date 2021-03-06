
namespace RedisStream.Ap.Model
{
    using System;
    using System.Threading.Tasks;
    using Autofac;
    using RedisStream.Applibs;
    using RedisStream.Model;

    public class ConsumerAProcess : IProcess
    {
        private string[] subscribeTopics = new string[] { "ProducerA" };

        public Task Execute()
        {
            return Task.Run(() =>
            {
                try
                {
                    var consumer = new RedisStreamConsumer(
                        this.subscribeTopics,
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
