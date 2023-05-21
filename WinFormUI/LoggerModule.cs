using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Extras.DynamicProxy;
using DRYDemoLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WinFormUI
{
    public class ModelInterfaceItem
    {
        public string LogType { get; set; }

        public string LogInterface{get; set; }
        public bool UseLogger { get; set; }
    }

public class LoggerModule : Module
    {
        public List<ModelInterfaceItem> ModelInterfaces { get; set; }
        //public bool UseLogger { get; set; }
        //public string Processor { get; set; }
        //public string ProcessorInterface { get; set; }
        public string LoggerInterceptor { get; set; }
        public string LoggerType { get; set; }
        public string LoggerInterface { get; set; }
        public string MainWin { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var loggerInterceptorType = Type.GetType(LoggerInterceptor);
            var loggerType = Type.GetType(LoggerType);
            var loggerInterface = Type.GetType(LoggerInterface);

            builder.Register(c => Activator.CreateInstance(loggerType)).As(loggerInterface);
            builder.Register(c => Activator.CreateInstance(loggerInterceptorType));
            var mainWinType = Type.GetType(MainWin);

            builder.Register(c => Activator.CreateInstance(mainWinType));
            builder.Properties.Add("mainWinType", mainWinType);
            foreach (var modelInterFaceItem in ModelInterfaces)
            {
                var logType = Type.GetType(modelInterFaceItem.LogType);
                var logInterfaceType = Type.GetType(modelInterFaceItem.LogInterface);
                var b = builder.Register(c => Activator.CreateInstance(logType)).As(logInterfaceType);
                if(modelInterFaceItem.UseLogger)
                    b.EnableInterfaceInterceptors().InterceptedBy(loggerInterceptorType);
            }

            //            builder.Register(c => Activator.CreateInstance(mainWinType));

            //builder.RegisterType<MondLogger>().As<DRYDemoLibrary.ILogger>();
            //builder.RegisterType<LoggerInterceptor>();
            //builder.RegisterType<MainWindow>();
            //builder.RegisterType<EmployeeSecondProcessor>().As<IEmployeeProcessor>()
            //    .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
            //builder.RegisterType<JsonFileStorage>().As<IModelStorage>()
            //    .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggerInterceptor));
        }
    }
}
