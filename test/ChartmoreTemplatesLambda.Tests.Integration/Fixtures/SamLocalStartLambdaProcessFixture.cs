using System;
using System.Threading;
using Amazon.Lambda;
using CliWrap;

namespace ChartmoreTemplatesLambda.Tests.Integration.Fixtures;

public class SamLocalStartLambdaProcessFixture : IDisposable
{
    public AmazonLambdaClient Client { get; }

    private CommandTask<CommandResult> _cliTask;
    private readonly CancellationTokenSource _cancellationTokenSource;
    public SamLocalStartLambdaProcessFixture()
    {
        _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(2));

        var isDebug = System.Diagnostics.Debugger.IsAttached;
        
        var stdout = isDebug ? PipeTarget.ToDelegate(Console.WriteLine) : PipeTarget.ToFile("output.stdout.txt");
        var stderr = isDebug ? PipeTarget.ToDelegate(Console.WriteLine) : PipeTarget.ToFile("output.stderr.txt"); 
        
        _cliTask = Cli.Wrap("sam")
            .WithArguments("local start-lambda")
            .WithWorkingDirectory(AppDomain.CurrentDomain.BaseDirectory)
            .WithStandardOutputPipe(stdout)
            .WithStandardErrorPipe(stderr)
            .ExecuteAsync(_cancellationTokenSource.Token);
        
        Client = new AmazonLambdaClient(new AmazonLambdaConfig
        {
            ServiceURL = "http://localhost:3001"
        });
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();

        _cliTask.ConfigureAwait(false).GetAwaiter().GetResult();
    }
}