using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace djfoxer.PerformanceDotNet.App.Benchmark.Helpers
{
    [SimpleJob(RuntimeMoniker.Net481, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net90)]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [MarkdownExporterAttribute.GitHub]
    public abstract class BaseBenchmark
    {
    }
}
