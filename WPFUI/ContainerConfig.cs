using Autofac;
using Autofac.Extras.DynamicProxy;
using DRYDemoLibrary;
using ModelsLib;
using ModelsLib.Models;
using ModelsLib.Storage;
using Splat;
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
            builder.RegisterType<MondLogger>().As<DRYDemoLibrary.ILogger>();
            builder.RegisterType<LoggerInterceptor>();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<XmlFileStorage>().As<ISave>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
            builder.RegisterType<XmlFileStorage>().As<ILoad>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
            builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
            return builder.Build();
        }
    }
}
