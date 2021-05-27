using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace Monitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
         try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                   .Build();

                var servicesProvider = BuildDi(config);
                
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<Runner>();
                    runner.DoAction("Action1");
                    int counter = 0;
                    while (true)
                    {
                        counter++;
                        Console.WriteLine("Output via Console: " + counter);
                        Thread.Sleep(2000);
                        logger.Info("Thread is Running as Info: " + counter);
                        Thread.Sleep(1000);
                        logger.Debug("Thread is Running as Debug: " + counter);
                    }
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }
        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<Runner>() // Runner is the custom class
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
