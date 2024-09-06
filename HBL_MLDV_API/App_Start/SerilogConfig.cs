using Serilog;
using Serilog.Core;

namespace BussinessApi.App_Start
{
    public class SerilogConfig
    {
        public static Logger Logger { get; set; }

        /// <summary>
        /// this method will configure serilog configuration
        /// </summary>
        public static void InitGlobalLogger()
        {
            var logPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/log.txt");
            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Logger = log;
        }
    }
}