using Autofac;
using Autofac.Extras.DynamicProxy;
using DRYDemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI;

namespace WinFormUI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<LoggerInterceptor>();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));

            return builder.Build();
        }
    }
}
