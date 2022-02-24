
namespace RedisStream.Ap.Applibs
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    internal static class ConfigHelper
    {
        /// <summary>
        /// Redis連線字串
        /// </summary>
        public static readonly string RedisConn = ConfigurationManager.ConnectionStrings["Redis"].ConnectionString;
    }
}
