
namespace RedisStream.Ap
{
    using System;
    using System.Linq;
    using RedisStream.Ap.Applibs;
    using RedisStream.Ap.Model;
    using RedisStream.Applibs;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RedisStreamFactory.Start(NoSqlService.RedisConnections);

                var cmd = string.Empty;

                if (!new string[] { "1", "2", "3", "4", "5"}.Contains(cmd))
                {
                    Console.WriteLine("1.ProducerA");
                    Console.WriteLine("2.ProducerB");
                    Console.WriteLine("3.ConsumerA");
                    Console.WriteLine("4.ConsumerB");
                    Console.WriteLine("5.ConsumerPlural");
                    Console.WriteLine("6.ConsumerGroupA");

                    cmd = Console.ReadLine();
                }

                IProcess process =
                    cmd == "1" ? new ProducerAProcess() :
                    cmd == "2" ? new ProducerBProcess() :
                    cmd == "3" ? new ConsumerAProcess() :
                    cmd == "4" ? new ConsumerBProcess() :
                    cmd == "5" ? new ConsumerPluralProcess() :
                    cmd == "6" ? new GroupConsumerAProcess() :
                    (IProcess)null;

                process?.Execute();
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
