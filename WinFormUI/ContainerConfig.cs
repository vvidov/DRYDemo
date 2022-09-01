using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using DRYDemoLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public static class ContainerConfig
    {
        public static Form MainForm { get; private set; }
        public static IContainer Configure()
        {

            var config = new ConfigurationBuilder();
            // config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
            // config.AddXmlFile comes from Microsoft.Extensions.Configuration.Xml
            config.AddJsonFile("autofac.json");

            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            //            var builder = new ContainerBuilder();
            //            builder.RegisterType<Logger>().As<ILogger>();
            //            builder.RegisterType<LoggerInterceptor>();
            //            builder.RegisterType<Dashboard>();
            //builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
            //    .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));

            var container = builder.Build();
            var mainFormType = builder.Properties["mainWinType"] as Type;
            //using (var scope = container.BeginLifetimeScope())
            //{
                MainForm = container.Resolve(mainFormType) as Form;
            //}

            return container;
        }
    }
}
