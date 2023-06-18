using System;
using Chartmore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChartmoreTemplates.Lambda.Tests.Unit;

public static class Setup
{
    public static ChartmoreContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ChartmoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ChartmoreContext(options);
    }
}