using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace djfoxer.DotNetFrameworkVsCore.Common
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [SimpleJob(RuntimeMoniker.Mono)]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [MarkdownExporterAttribute.GitHub]
    public class MainBenchmark
    {
        IEnumerable<int> _tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();
        byte[] _raw = new byte[100 * 1024 * 1024];
        SHA256 _sha = SHA256.Create();
        static string _s = "abcdefghijklmnopqrstuvwxyz";
        List<BookToSerialize> _books = null;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            for (int index = 0; index < _raw.Length; index++) _raw[index] = (byte)index;

            List<BookToSerialize> _books = null;
            _books = new List<BookToSerialize>();
            for (int i = 0; i < 1_00000; i++)
            {
                string id = i.ToString();
                _books.Add(new BookToSerialize { Name = id, Id = id });
            }
        }

        [Benchmark]
        public DayOfWeek EnumParse() => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Thursday");

        [Benchmark]
        public int LinqOrderBySkipFirst()
        {
            return _tenMillionToZero.OrderBy(i => i).Skip(4).First();
        }

        [Benchmark]
        public byte[] Sha256()
        {
            return _sha.ComputeHash(_raw);
        }

        [Benchmark]
        public bool StringStartsWith()
        {
            var data = false;
            for (int i = 0; i < 100_000_000; i++)
            {
                data = _s.StartsWith("abcdefghijklmnopqrstuvwxy-", StringComparison.Ordinal);
            }
            return data;
        }

        [Benchmark]
        public object Deserialize()
        {
            var formatter = new BinaryFormatter();
            var mem = new MemoryStream();
            formatter.Serialize(mem, _books);
            mem.Position = 0;

            return formatter.Deserialize(mem);
        }
    }
}
