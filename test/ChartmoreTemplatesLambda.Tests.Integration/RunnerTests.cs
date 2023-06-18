using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.CloudWatchEvents;
using Chartmore.Infrastructure.Data;
using ChartmoreTemplatesLambda.Tests.Integration.Fixtures;
using Xunit;

namespace ChartmoreTemplatesLambda.Tests.Integration;

public class RunnerTests : IClassFixture<DependenciesFixture>
{
    private readonly DependenciesFixture _fixture;
    
    public RunnerTests(DependenciesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Can_Store_Scan()
    {
        var function = _fixture.LambdaFunctionFixture.Function;

        await function.FunctionHandler(new CloudWatchEvent<object>());

        await using var context = _fixture.GetRequiredService<ChartmoreContext>();

        Assert.Equal(1, context.Scans.Count());
    }
}