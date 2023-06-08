using System;
using System.Threading.Tasks;
using Chartmore.Core;
using ChartmoreTemplatesLambda.Application;
using Moq;
using Xunit;

namespace ChartmoreTemplates.Lambda.Tests.Unit;

public class RunnerTests
{
    private readonly Runner _sut;
    
    public RunnerTests()
    {
        _sut = new Runner(new Mock<IChartmoreContext>().Object);
    }
    
    [Fact]
    public async Task Runner_ThrowsNotImplemented()
    {
        await Assert.ThrowsAsync<NotImplementedException>(_sut.RunAsync);
    }
}