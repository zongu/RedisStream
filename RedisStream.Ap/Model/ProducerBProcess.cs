
namespace RedisStream.Ap.Model
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using RedisStream.Ap.Model.Event;
    using RedisStream.Applibs;

    public class ProducerBProcess : IProcess
    {
        private const string topic = "ProducerB";

        private int count = 0;

        public Task Execute()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        RedisStreamProducer.Publish(topic, new MessageBEvent()
                        {
                            Message = $"MessageBEvent-{++count}"
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    SpinWait.SpinUntil(() => false, 5000);
                }
            });
        }
    }
}
