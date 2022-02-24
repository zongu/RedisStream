
namespace RedisStream.Ap
{
    using System;
    using RedisStream.Ap.Applibs;
    using RedisStream.Applibs;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RedisStreamFactory.Start(NoSqlService.RedisConnections);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Finished!!");
            Console.Read();
        }
    }
}
