using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.CloudWatchEvents;
using Amazon.Lambda.Core;
using Chartmore.Infrastructure;
using ChartmoreTemplatesLambda.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ChartmoreTemplatesLambda;

public class Function
{
    public const string Name = "ChartmoreTemplatesLambda";
    
    public async Task FunctionHandler(CloudWatchEvent<object> _)
    {
        using var host = CreateHostBuilder().Build();

        var runner = host.Services.GetRequiredService<IRunner>();

        await runner.RunAsync();
    }
        
    private static IHostBuilder CreateHostBuilder() => 
        Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddEnvironmentVariables($"{Name}_");
            })
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("AppConnection");
                
                services.AddSingleton<IRunner, Runner>();
                services.AddChartmoreContext(connectionString);
            });
}