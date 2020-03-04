using BenchmarkDotNet.Running;
using djfoxer.DotNetFrameworkVsCore.Common;
using System;

namespace djfoxer.DotNetFrameworkVsCore.Core
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
