using System;
using Chartmore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ChartmoreTemplatesLambda.Tests.Integration.Fixtures;

public class DependenciesFixture : IDisposable
{
    public PostgresDockerFixture PostgresDockerFixture { get; }
    public LambdaFunctionFixture LambdaFunctionFixture { get; }

    private IServiceScope _scope;
    
    public DependenciesFixture()
    {
        PostgresDockerFixture = new PostgresDockerFixture();
        LambdaFunctionFixture = new LambdaFunctionFixture();
        
         var serviceBuilder = new ServiceCollection()
             .AddSingleton(LambdaFunctionFixture.Function)
             .AddChartmoreContext(PostgresDockerFixture.ConnectionString);
         
         Environment.SetEnvironmentVariable($"{Function.Name}_ConnectionStrings__AppConnection", PostgresDockerFixture.ConnectionString);
        
         var services = serviceBuilder.BuildServiceProvider();
        
         _scope = services.CreateScope();
    }

    public T GetRequiredService<T>() where T : notnull => _scope.ServiceProvider.GetRequiredService<T>();

    public void Dispose()
    {
        _scope.Dispose();
        PostgresDockerFixture.Dispose();
    }
}