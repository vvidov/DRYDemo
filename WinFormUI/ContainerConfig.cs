using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using DRYDemoLibrary;
using Microsoft.Extensions.Configuration;
using ModelsLib;
using ModelsLib.Models;
using ModelsLib.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

            //Type t = typeof(IModelStorage);
            //Debug.WriteLine(t.AssemblyQualifiedName);
            var config = new ConfigurationBuilder();
            // config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
            // config.AddXmlFile comes from Microsoft.Extensions.Configuration.Xml
            config.AddJsonFile("autofac.json");

            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            //var loggerType = Type.GetType(builder.Properties["LoggerType"] as string);
            //var LoggerInterface = Type.GetType(builder.Properties["LoggerInterface"] as string);
            //builder.Register(c =>Activator.CreateInstance(loggerType)).As(LoggerInterface);
            //builder.RegisterType<LoggerInterceptor>();
            //builder.Register(c => Activator.CreateInstance(builder.Properties["mainWinType"] as Type));
            //var LogingInterfaces = builder.Properties["LogingInterfaces"];
            //foreach (var property in builder.Properties["LogingInterfaces"] as List<LogingInterfaces>())
            //{

            //}
            //builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
            //    .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
            //builder.RegisterType<JsonFileStorage>().As<IModelStorage>()
            //    .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));

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

        //public static IContainer Configure()
        //{
        //    Type t = typeof(IPersonModelStorage);
        //    Debug.WriteLine(t.AssemblyQualifiedName);

        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<MondLogger>().As<DRYDemoLibrary.ILogger>();
        //    builder.RegisterType<LoggerInterceptor>();
        //    builder.RegisterType<Dashboard2RUI>();
        //    builder.RegisterType<XmlFileStorage>().As<IPersonModelStorage>()
        //        .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
        //    builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
        //        .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
        //    System.Diagnostics.Debug.WriteLine(typeof(IPersonModelStorage).AssemblyQualifiedName);
        //    return builder.Build();
        //}

    }
}
