using BenchmarkDotNet.Running;
using djfoxer.DotNetFrameworkVsCore.Common;

namespace djfoxer.DotNetFrameworkVsCore.App
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RegexBenchmark>();
            BenchmarkRunner.Run<MainBenchmark>();
        }
    }
}
