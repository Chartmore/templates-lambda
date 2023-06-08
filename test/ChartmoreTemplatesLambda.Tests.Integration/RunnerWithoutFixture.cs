using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Chartmore.Infrastructure.Data;
using ChartmoreTemplatesLambda.Tests.Integration.Fixtures;
using Xunit;

namespace ChartmoreTemplatesLambda.Tests.Integration;

public class RunnerWithoutFixture
{
    private static string GetPayloadFromJsonFile()
    {
        try
        {
            var stack = new StackTrace();
            var frame = stack.GetFrame(1);
            var rawName = frame.GetMethod().ReflectedType.Name;

            var regex = new Regex("<(.*?)>");
            var methodName = regex.Match(rawName).Groups.Values.Last().Value;
            
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Events", methodName.ToLower() + ".json");

            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            throw new FileLoadException("Could not find file");
        }
    }
    
    [Fact]
    public async Task Can_Store_Scan()
    {
        var payload = GetPayloadFromJsonFile();

        var client = new AmazonLambdaClient(new AmazonLambdaConfig
        {
            ServiceURL = "http://localhost:3001"
        });

        var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(10)).Token;
        
        var response = await client.InvokeAsync(new InvokeRequest
        {
            FunctionName = "ChartmoreTemplatesLambdaFunction",
            InvocationType = InvocationType.RequestResponse,
            Payload = payload
        }, cancellationToken);
        
        Assert.Equal(200, response.StatusCode);
    }
}