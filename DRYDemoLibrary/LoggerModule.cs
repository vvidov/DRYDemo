using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Extras.DynamicProxy;
using System;
using System.Linq;

namespace DRYDemoLibrary
{

    public class LoggerModule : Module
    {
        public bool UseLogger { get; set; }
        public string Processor { get; set; }
        public string ProcessorInterface { get; set; }
        public string LoggerInterceptor { get; set; }
        public string MainWin { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var processorType = Type.GetType(Processor);
            var processorInterfaceType = Type.GetType(ProcessorInterface);
            var loggerInterceptorType = Type.GetType(LoggerInterceptor);

            var mainWinType = Type.GetType(MainWin);
            //var mainWin = Activator.CreateInstance(mainWinType);
            builder.Register(c => Activator.CreateInstance(mainWinType));
            builder.Properties.Add("mainWinType", mainWinType);

            var b = builder.Register(c => Activator.CreateInstance(processorType)).As(processorInterfaceType);
            if (UseLogger)
                b.EnableInterfaceInterceptors().InterceptedBy(loggerInterceptorType);
        }
    }
}
