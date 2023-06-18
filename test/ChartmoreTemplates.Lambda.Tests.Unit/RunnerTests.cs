using System;
using System.Linq;
using System.Threading.Tasks;
using Chartmore.Core;
using Chartmore.Infrastructure.Data;
using ChartmoreTemplatesLambda.Application;
using Moq;
using Xunit;

namespace ChartmoreTemplates.Lambda.Tests.Unit;

public class RunnerTests : IDisposable
{
    private readonly ChartmoreContext _context;
    private readonly Runner _sut;
    
    public RunnerTests()
    {
        _context = Setup.CreateContext();
        _sut = new Runner(_context);
    }
    
    [Fact]
    public async Task Runner_Saves_New_Scan()
    {
        await _sut.RunAsync();

        Assert.Equal(1, _context.Scans.Count());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}