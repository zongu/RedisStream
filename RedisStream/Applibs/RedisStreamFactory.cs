
namespace RedisStream.Applibs
{
    using System;
    using StackExchange.Redis;

    /// <summary>
    /// Redis stream工廠
    /// </summary>
    public static class RedisStreamFactory
    {
        private static ConnectionMultiplexer redisConn;

        private static int _dataBase;

        private static bool isWork = false;

        public static bool IsWork
            => isWork;

        public static void Start(ConnectionMultiplexer conn, int dataBase = 15)
        {
            if (redisConn != null)
            {
                return;
            }

            redisConn = conn;
            _dataBase = dataBase;
            isWork = true;
        }

        public static void Stop()
        {
            if (redisConn == null)
            {
                return;
            }

            isWork = false;
            redisConn.Close();
            redisConn.Dispose();
        }

        public static T UseConnection<T>(Func<IDatabase, T> func)
        {
            if (redisConn == null)
            {
                throw new Exception("Please execute RedisStreamFactory.Start first");
            }

            var redis = redisConn.GetDatabase(_dataBase);
            return func(redis);
        }
    }
}
