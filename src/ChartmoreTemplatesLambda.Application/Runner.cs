namespace ChartmoreTemplatesLambda.Application;

public class Runner : IRunner
{
    public Task RunAsync()
    {
        throw new NotImplementedException("custom error");
    }
}

public interface IRunner
{
    Task RunAsync();
}