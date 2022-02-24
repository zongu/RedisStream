
namespace RedisStream.Ap.Applibs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Features.Indexed;
    using RedisStream.Applibs;
    using RedisStream.Model;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        public static void Register()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.IsAssignableTo<IRedisStreamHandler>())
                .Named<IPubSubHandler<RedisStreamEventStream>>(t => t.Name.Replace("Handler", string.Empty))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.Register(p => new RedisStreamDispatcher<RedisStreamEventStream>(
                    ErrorCallBack,
                    p.Resolve<IIndex<string, IPubSubHandler<RedisStreamEventStream>>>()))
                .As<IPubSubDispatcher<RedisStreamEventStream>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }

        private static void ErrorCallBack(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
