using System;

namespace ChartmoreTemplatesLambda.Tests.Integration.Fixtures;

public class LambdaFunctionFixture
{
    public Function Function { get; }

    public LambdaFunctionFixture()
    {
        Function = new Function();
    }
}