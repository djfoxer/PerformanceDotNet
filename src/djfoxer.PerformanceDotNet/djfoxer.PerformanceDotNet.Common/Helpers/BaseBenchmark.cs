using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace djfoxer.PerformanceDotNet.Common.Helpers
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Mono)]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [MarkdownExporterAttribute.GitHub]
    public abstract class BaseBenchmark
    {
        
    }
}
