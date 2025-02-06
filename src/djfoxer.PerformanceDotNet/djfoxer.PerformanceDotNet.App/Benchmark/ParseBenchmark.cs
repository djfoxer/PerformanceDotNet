﻿using BenchmarkDotNet.Attributes;
using djfoxer.PerformanceDotNet.App.Benchmark.Helpers;
using System;
using System.Linq;
using System.Numerics;

namespace djfoxer.PerformanceDotNet.App.Benchmark
{
    public class ParseBenchmark : BaseBenchmark
    {
        string _bingIntToParse;
        [GlobalSetup(Target = nameof(ParseBigInt))]
        public void SetupRead() => _bingIntToParse = string.Concat(Enumerable.Repeat("654719003", 50));

        [Benchmark]
        public DayOfWeek EnumParse() => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Thursday");

        [Benchmark]
        public BigInteger ParseBigInt() => BigInteger.Parse(_bingIntToParse);
    }
}
