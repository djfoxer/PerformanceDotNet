using BenchmarkDotNet.Attributes;
using djfoxer.PerformanceDotNet.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace djfoxer.PerformanceDotNet.Common
{
    public class ParseBenchmark : BaseBenchmark
    {
        public IEnumerable<object> BingIntToParse { get; } = new string[]
        {
            "1234",
            int.MinValue.ToString(),
            decimal.MinValue.ToString(),
            string.Concat(Enumerable.Repeat("1234567890", 20)),
            string.Concat(Enumerable.Repeat("654719003", 50)),
        };

        [Benchmark]
        public DayOfWeek EnumParse() => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Thursday");

        [Benchmark]
        [ArgumentsSource(nameof(BingIntToParse))]
        public BigInteger Parse(string numberString) => BigInteger.Parse(numberString);
    }
}
