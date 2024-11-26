using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Linq;

namespace DRYDemoLibrary
{
    public class LoggerInterceptor : IInterceptor
    {
        ILogger _logger;

        public LoggerInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.Log(string.Format("Calling method {0} with parameters {1}... ",
              invocation.Method.Name,
              string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())));

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                invocation.Proceed();
            }
            catch(Exception ex)
            {
                _logger.Log(string.Format("Exception {0}. \n{1}", ex.Message, ex.StackTrace));
            }
            stopWatch.Stop();

            _logger.Log(string.Format("Done: result was {0}. It took {1}", invocation.ReturnValue, stopWatch.ElapsedTicks));
        }
    }
}
