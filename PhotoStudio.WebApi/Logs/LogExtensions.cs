using Serilog;
using Serilog.Sinks.Graylog;
using System.Runtime.CompilerServices;

namespace PhotoStudio.WebApi.Logs
{
    /// <summary>
    /// Log extensions method
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Configure Serilog to write in GrayLog
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <param name="graylogLoggerConfiguration"></param>
        /// <returns></returns>
        public static LoggerConfiguration AddGraylogLogger(this LoggerConfiguration loggerConfiguration, GraylogLoggerConfiguration graylogLoggerConfiguration) {

            return graylogLoggerConfiguration.Enabled ?
                   loggerConfiguration.WriteTo.Graylog(graylogLoggerConfiguration.Host, graylogLoggerConfiguration.Port,
                   Serilog.Sinks.Graylog.Core.Transport.TransportType.Udp, graylogLoggerConfiguration.MinimumLevel) : loggerConfiguration;

        }

        /// <summary>
        /// ConfigureLogger
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder) { 

            return builder.UseSerilog((context, loggerInformation) => ConfigureSerilogLogger(loggerInformation, context.Configuration));
        }

        /// <summary>
        /// ConfigureSerilogLogger
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static LoggerConfiguration ConfigureSerilogLogger(LoggerConfiguration loggerConfiguration, IConfiguration configuration) {

            GraylogLoggerConfiguration graylogLogger = new GraylogLoggerConfiguration();
            configuration.GetSection("Logging:Graylog").Bind(graylogLogger);

            return loggerConfiguration.AddGraylogLogger(graylogLogger);
            
        }
            
    }
}
