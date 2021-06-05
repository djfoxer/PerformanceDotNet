using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace djfoxer.PerformanceDotNet.Common
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Mono)]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [MarkdownExporterAttribute.GitHub]
    public class RegexBenchmark
    {
        string _commonInput = string.Empty;
        Regex _regexEmail, _regexStrongPassword, _regexSpanSearching, _regexBackTracking;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            ResourceManager rm = new ResourceManager(typeof(RegexBenchmark).Namespace + ".Resources.InputResource", Assembly.GetExecutingAssembly());
            _commonInput = rm.GetString("CSharpUseDeconstructionDiagnosticAnalyzer_cs", System.Globalization.CultureInfo.InvariantCulture);

            _regexEmail = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled);
            _regexStrongPassword = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", RegexOptions.Compiled);
            _regexSpanSearching = new Regex("([ab]cd|ef[g-i])jklm", RegexOptions.Compiled);
            _regexBackTracking = new Regex("a*a*a*a*a*a*a*b", RegexOptions.Compiled);
        }

        [Benchmark]
        public bool Regex_Email()
        {
            return _regexEmail.IsMatch(_commonInput);
        }

        [Benchmark]
        public bool Regex_StrongPassword()
        {
            return _regexStrongPassword.IsMatch(_commonInput);
        }

        [Benchmark]
        public bool Regex_SpanSearching()
        {
            return _regexSpanSearching.IsMatch(_commonInput);
        }

        [Benchmark]
        public bool Regex_BackTracking()
        {
            return _regexBackTracking.IsMatch("aaaaaaaaaaaaaaaaaaaaa");
        }
    }
}
