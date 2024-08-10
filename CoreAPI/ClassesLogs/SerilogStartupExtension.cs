using Serilog.Events;
using Serilog;
using Serilog.Core;
using Serilog.Formatting;


namespace CoreAPI.ClassesLogs
{
    public static class SerilogStartupExtension

    {
        public static void AddSerilogApi(WebApplicationBuilder builder)
        {
   
            string shortdate = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string path = builder.Configuration.GetSection("LoggerBasePath").Value;
            string fileName = $@"{path}\{shortdate}.log";
            string template = builder.Configuration.GetSection("LoggerFileTemplate").Value;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Enrich.WithClientIp()
                .Enrich.WithCorrelationId()
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Console()
                .WriteTo.File(fileName, outputTemplate: template)
                .CreateLogger();
           
            builder.Host.UseSerilog(Log.Logger);

           
        }

    }

   
}

