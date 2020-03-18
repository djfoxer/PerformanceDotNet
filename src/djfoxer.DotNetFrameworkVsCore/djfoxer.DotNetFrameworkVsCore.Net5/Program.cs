using BenchmarkDotNet.Running;
using djfoxer.DotNetFrameworkVsCore.Common;
using System;

namespace djfoxer.DotNetFrameworkVsCore.Net5
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MainBenchmark>();
            Console.ReadLine();
        }
    }
}
