using BenchmarkDotNet.Running;
using djfoxer.PerformanceDotNet.Common;

namespace djfoxer.PerformanceDotNet.App
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<RegexBenchmark>();
            BenchmarkRunner.Run<MainBenchmark>();
        }
    }
}
