using BenchmarkDotNet.Running;
using djfoxer.DotNetFrameworkVsCore.Common;
using System;

namespace djfoxer.DotNetFrameworkVsCore.Framework
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
