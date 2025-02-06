using BenchmarkDotNet.Attributes;
using djfoxer.PerformanceDotNet.App.Benchmark.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace djfoxer.PerformanceDotNet.App.Benchmark
{

    public class MainBenchmark : BaseBenchmark
    {
        readonly IEnumerable<int> _tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();
        readonly byte[] _rawBytes = new byte[100 * 1024 * 1024];
        readonly HashAlgorithm _sha = SHA256.Create();
        const string StringToTest = "abcdefghijklmnopqrstuvwxyz";
        readonly List<BookToSerialize> _books = new List<BookToSerialize>();

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            //Sha256
            for (int index = 0; index < _rawBytes.Length; index++) _rawBytes[index] = (byte)index;

            //Deserialize
            _books.Clear();
            for (int i = 0; i < 1_00000; i++)
            {
                string id = i.ToString();
                _books.Add(new BookToSerialize { Name = id, Id = id });
            }
        }

        [Benchmark]
        public int LinqOrderBySkipFirst()
        {
            return _tenMillionToZero.OrderBy(i => i).Skip(4).First();
        }

        [Benchmark]
        public byte[] Sha256()
        {
            return _sha.ComputeHash(_rawBytes);
        }

        [Benchmark]
        public bool StringStartsWith()
        {
            var data = false;
            for (int i = 0; i < 100_000_000; i++)
            {
                data = StringToTest.StartsWith("abcdefghijklmnopqrstuvwxy-", StringComparison.Ordinal);
            }
            return data;
        }

        [Benchmark]
        public object DesrializeSystemTextJson()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(_books);
            return System.Text.Json.JsonSerializer.Deserialize<List<BookToSerialize>>(json);
        }

        [Benchmark]
        public object DesrializeNewtonsoftJson()
        {
            var json = JsonConvert.SerializeObject(_books);
            return JsonConvert.DeserializeObject<List<BookToSerialize>>(json);
        }
    }
}
