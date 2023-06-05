using System;
using System.Threading.Tasks;
using ChartmoreTemplatesLambda.Application;
using Xunit;

namespace ChartmoreTemplates.Lambda.Tests.Unit;

public class RunnerTests
{
    private readonly Runner _sut;
    
    public RunnerTests()
    {
        _sut = new Runner();
    }
    
    [Fact]
    public async Task Runner_ThrowsNotImplemented()
    {
        await Assert.ThrowsAsync<NotImplementedException>(_sut.RunAsync);
    }
}