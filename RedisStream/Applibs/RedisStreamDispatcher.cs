
namespace RedisStream.Applibs
{
    using System;
    using Autofac.Features.Indexed;
    using RedisStream.Model;

    /// <summary>
    /// Redis stream調度員
    /// </summary>
    public class RedisStreamDispatcher<TEventStream> : IPubSubDispatcher<TEventStream>
        where TEventStream : EventStream
    {
        /// <summary>
        /// handler集合
        /// </summary>
        private IIndex<string, IRedisStreamHandler<EventStream>> handlerSets;

        /// <summary>
        /// 發生錯誤時 callback回寫
        /// </summary>
        private Action<Exception> errorCallBack;

        public RedisStreamDispatcher(Action<Exception> errorCallBack, IIndex<string, IRedisStreamHandler<EventStream>> handlerSets)
        {
            this.handlerSets = handlerSets;
            this.errorCallBack = errorCallBack;
        }

        public bool DispatchMessage(TEventStream stream)
        {
            try
            {
                if (this.handlerSets.TryGetValue(stream.Type, out var handler))
                {
                    return handler.Handle(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                this.errorCallBack(ex);
                return false;
            }
        }
    }
}
