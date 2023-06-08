using System;
using Chartmore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ChartmoreTemplatesLambda.Tests.Integration.Fixtures;

public class DependenciesFixture : IDisposable
{
    public PostgresDockerFixture PostgresDockerFixture { get; }
    public SamLocalStartLambdaProcessFixture SamLocalStartLambdaProcessFixture { get; }
    
    public IServiceScope Scope { get; }
    
    public DependenciesFixture()
    {
        PostgresDockerFixture = new PostgresDockerFixture();
        SamLocalStartLambdaProcessFixture = new SamLocalStartLambdaProcessFixture();
        
         var serviceBuilder = new ServiceCollection()
             .AddSingleton(SamLocalStartLambdaProcessFixture.Client)
             .AddChartmoreContext(PostgresDockerFixture.ConnectionString);
        
         var services = serviceBuilder.BuildServiceProvider();
        
         Scope = services.CreateScope();
    }

    public T GetRequiredService<T>() where T : notnull => Scope.ServiceProvider.GetRequiredService<T>();

    public void Dispose()
    {
        Scope.Dispose();
        PostgresDockerFixture.Dispose();
        SamLocalStartLambdaProcessFixture.Dispose();
    }
}