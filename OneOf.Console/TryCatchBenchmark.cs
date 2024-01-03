using BenchmarkDotNet.Attributes;

namespace OneOf.Console;

[MemoryDiagnoser]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class TryCatchBenchmark
{
    private static int GetRandomNumber()
    {
        return Random.Shared.Next(0, 2);
    }

    [Benchmark(Baseline = true)]
    public void NoTryCatch()
    {
        int i = GetNumber();
    }

    [Benchmark]
    public void TryCatch_NoException()
    {
        try
        {
            int i = GetNumber();
        }
        catch (Exception)
        {
        }
    }

    [Benchmark]
    public void TryCatch_WithException()
    {
        try
        {
            int i = GetNumber(true);
        }
        catch (Exception)
        {
        }
    }

    [Benchmark]
    public void TryCatch_WithSomeException()
    {
        try
        {
            var random = GetRandomNumber();
            int i = GetNumber(random == 0);
        }
        catch (Exception)
        {
        }
    }


    private static int GetNumber(bool throwException = false)
    {
        return throwException ? throw new Exception() : 1;
    }
}
