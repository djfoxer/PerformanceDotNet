using BenchmarkDotNet.Running;
using djfoxer.PerformanceDotNet.Common.Helpers;

namespace djfoxer.PerformanceDotNet.App
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(BaseBenchmark).Assembly).Run(args);
        }
    }
}
