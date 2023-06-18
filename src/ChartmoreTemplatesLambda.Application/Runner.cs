using Chartmore.Core;
using Chartmore.Core.Entities;

namespace ChartmoreTemplatesLambda.Application;

public class Runner : IRunner
{
    private readonly IChartmoreContext _chartmoreContext;

    public Runner(IChartmoreContext chartmoreContext)
    {
        _chartmoreContext = chartmoreContext;
    }

    public async Task RunAsync()
    {
        await _chartmoreContext.Scans.AddAsync(new Scan
        {
            Id = Guid.NewGuid().ToString(),
        });

        await _chartmoreContext.SaveChangesAsync(CancellationToken.None);
    }
}

public interface IRunner
{
    Task RunAsync();
}