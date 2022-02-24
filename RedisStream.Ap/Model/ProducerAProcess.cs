
namespace RedisStream.Ap.Model
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using RedisStream.Ap.Model.Event;
    using RedisStream.Applibs;

    public class ProducerAProcess : IProcess
    {
        private const string topic = "ProducerA";

        private int count = 0;

        public Task Execute()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        RedisStreamProducer.Publish(topic, new MessageAEvent()
                        {
                            Message = $"MessageAEvent-{++count}"
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
