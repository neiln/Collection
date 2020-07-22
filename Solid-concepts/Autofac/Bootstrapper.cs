using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Solid_concepts
{
    public static class Bootstrapper
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileLogger>().As<IFileLogger>();
            builder.RegisterType<EventLogger>().As<IEventLogger>();
            builder.RegisterType<AppConfig>().As<IAppConfig>().SingleInstance();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<DbConnection>().As<DbConnection>();
            builder.RegisterType<DataAccess>().SingleInstance();

            return builder.Build();
        }
    }
}
