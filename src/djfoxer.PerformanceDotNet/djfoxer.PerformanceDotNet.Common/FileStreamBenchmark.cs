using BenchmarkDotNet.Attributes;
using djfoxer.PerformanceDotNet.Common.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace djfoxer.PerformanceDotNet.Common
{
    public class FileStreamBenchmark : BaseBenchmark
    {
        const int FileSize = 1_000_000;
        const string _fileName = "file.txt";
        readonly byte[] _buffer = new byte[8_000];

        [GlobalSetup(Target = nameof(ReadAsync))]
        public void SetupRead() => File.WriteAllBytes(_fileName, new byte[FileSize]);

        [Benchmark]
        public async ValueTask ReadAsync()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                while (await fileStream.ReadAsync(_buffer, 0, _buffer.Length) > 0)
                {
                }
            }
        }

        [Benchmark]
        public async ValueTask WriteAsync()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                for (int i = 0; i < FileSize / _buffer.Length; i++)
                {
                    await fileStream.WriteAsync(_buffer, 0, _buffer.Length);
                }
            }
        }

        [GlobalCleanup]
        public void Cleanup() => File.Delete(_fileName);
    }
}
