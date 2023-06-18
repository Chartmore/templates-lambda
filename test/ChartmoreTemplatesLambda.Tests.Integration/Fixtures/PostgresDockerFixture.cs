using System;
using Chartmore.Infrastructure.Data;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly;

namespace ChartmoreTemplatesLambda.Tests.Integration.Fixtures;

public class PostgresDockerFixture : IDisposable
{
    private readonly IContainerService container;

    public string ConnectionString { get; }
    
    public PostgresDockerFixture()
    {
        const string username = "postgres", password = "password", database = "integrationtests";
        
        container = new Builder().UseContainer()
            .UseImage("postgres:latest")
            .ExposePort(5432)
            .WithEnvironment($"POSTGRES_USER={username}", $"POSTGRES_PASSWORD={password}")
            .WaitForPort("5432/tcp", TimeSpan.FromSeconds(30))
            .Build()
            .Start();

        // Retrieve the container IP and port to construct the connection string
        var containerEndpoint = container.ToHostExposedEndpoint("5432/tcp"); 
        ConnectionString = $"Host=localhost;Port={containerEndpoint.Port};Database={database};User Id={username};Password={password};";

        Policy.Handle<NpgsqlException>()
            .WaitAndRetry(2, retryNum => retryNum * TimeSpan.FromMilliseconds(500))
            .Execute(() =>
            {
                using var context = new ChartmoreContext(new DbContextOptionsBuilder<ChartmoreContext>()
                    .UseNpgsql(ConnectionString)
                    .Options);

                context.Database.EnsureCreated();
            });
    }

    public void Dispose()
    {
        // Stop and remove the PostgreSQL container
        container.StopOnDispose = true;
        container.Dispose();
    }
}