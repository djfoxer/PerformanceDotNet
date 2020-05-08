# DotNetFrameworkVsCore - compare multiple .NET runtimes
Small console application to compare performance of **.NET Framework (4.8), .NET Core (3.1.x) and .NET 5 (preview 2)**. Common benchmarks are written in .NET Standard. App contains 5 benchmarks to test performance of .NET Framework (4.8), .NET Core (3.1.x) and .NET 5 (preview 2). Another 4 benchmarks are related to regex performance. Results taken by [BenchmarkDotNet](https://benchmarkdotnet.org/).

Blog post about comparing .NET runtimes performance [.NET Core is dead, long live the .NET Core (and .NET 5) and its performance](https://www.dobreprogramy.pl/djfoxer/Umarl-NET-Framework-niech-zyje-NET-Core-oraz-NET-i-jego-wydajnosc,105443.html) (only in Polish!)

### You can easily  check how fast/slow is .NET Framework/.NET Core/.NET 5!


**Try .NET 5 preview 2!** - .NET 5 is fresh, shiny and ready to test. To check new runtime you have to download [Visual Studio 2019 16.6.0 preview](https://visualstudio.microsoft.com/en/vs/preview/) and [.NET 5.0 Preview 2 SDK](https://dotnet.microsoft.com/download/dotnet-core/5.0). In this release .NET Team was focused to improve regex engine.



# .NET Framework (4.8) vs .NET Core (3.1.x) vs .NET 5 (preview 2)

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4702MQ CPU 2.20GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.2.20176.6
  [Host]     : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Job-XTAQMF : .NET Framework 4.8 (4.8.4150.0), X64 RyuJIT
  Job-XPHTQE : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Job-YFAAUM : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT


```
|               Method |       Runtime |               Mean | Ratio |
|--------------------- |-------------- |-------------------:|------:|
|            EnumParse |      .NET 4.8 |           277.9 ns |  1.00 |
|            EnumParse | .NET Core 3.1 |           164.3 ns |  0.59 |
|            EnumParse | .NET Core 5.0 |           161.3 ns |  0.58 |
|                      |               |                    |       |
| LinqOrderBySkipFirst |      .NET 4.8 | 1,818,312,740.0 ns |  1.00 |
| LinqOrderBySkipFirst | .NET Core 3.1 |   228,086,635.6 ns |  0.13 |
| LinqOrderBySkipFirst | .NET Core 5.0 |   222,083,673.8 ns |  0.12 |
|                      |               |                    |       |
|               Sha256 |      .NET 4.8 | 1,028,103,786.7 ns |  1.00 |
|               Sha256 | .NET Core 3.1 |   500,472,964.3 ns |  0.49 |
|               Sha256 | .NET Core 5.0 |   504,334,426.7 ns |  0.49 |
|                      |               |                    |       |
|     StringStartsWith |      .NET 4.8 | 1,918,666,640.0 ns |  1.00 |
|     StringStartsWith | .NET Core 3.1 |   880,179,750.0 ns |  0.46 |
|     StringStartsWith | .NET Core 5.0 |   858,676,357.1 ns |  0.45 |
|                      |               |                    |       |
|          Deserialize |      .NET 4.8 |   980,524,740.0 ns |  1.00 |
|          Deserialize | .NET Core 3.1 |   421,885,039.1 ns |  0.43 |
|          Deserialize | .NET Core 5.0 |   415,699,686.8 ns |  0.43 |
|                      |               |                    |       |
|          Regex_Email |      .NET 4.8 |  2,299,723.7 ns |  1.00 |
|          Regex_Email | .NET Core 3.1 |  1,845,451.4 ns |  0.80 |
|          Regex_Email | .NET Core 5.0 |    961,795.4 ns |  0.51 |
|                      |               |                 |       |
| Regex_StrongPassword |      .NET 4.8 |      1,887.3 ns |  1.00 |
| Regex_StrongPassword | .NET Core 3.1 |      1,726.2 ns |  0.91 |
| Regex_StrongPassword | .NET Core 5.0 |        427.4 ns |  0.23 |
|                      |               |                 |       |
|  Regex_SpanSearching |      .NET 4.8 |    339,303.0 ns |  1.00 |
|  Regex_SpanSearching | .NET Core 3.1 |    295,767.1 ns |  0.87 |
|  Regex_SpanSearching | .NET Core 5.0 |     22,660.0 ns |  0.07 |
|                      |               |                 |       |
|   Regex_BackTracking |      .NET 4.8 | 43,722,439.1 ns | 1.000 |
|   Regex_BackTracking | .NET Core 3.1 | 34,763,742.9 ns | 0.809 |
|   Regex_BackTracking | .NET Core 5.0 |        578.1 ns | 0.000 |

![Chart](img/cmp_1_1.png)
![Chart](img/cmp_1_2.png)
![Chart](img/cmp_1_3.png)
![Chart](img/cmp_1_4.png)
![Chart](img/cmp_1_5.png)
![Chart](img/cmp_1_6.png)

## Code details

### Enum:
```csharp
public DayOfWeek EnumParse() => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Thursday");
```

### Linq:
```csharp
//IEnumerable<int> _tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();

public void LinqOrderBySkipFirst() => _tenMillionToZero.OrderBy(i => i).Skip(4).First();
```

### SHA256:
```csharp
//byte[] _raw = new byte[100 * 1024 * 1024];
//for (int index = 0; index < _raw.Length; index++) _raw[index] = (byte)index;

public void Sha256() => _sha256.ComputeHash(_raw);
```

### String:
```csharp
// static string _s = "abcdefghijklmnopqrstuvwxyz";

public void StringStartsWith()
{
    for (int i = 0; i < 100_000_000; i++)
    {
        _s.StartsWith("abcdefghijklmnopqrstuvwxy-", StringComparison.Ordinal);
    }
}
```

### Deserialize:
```csharp
public void Deserialize()
{
    var books = new List<Book>();
    for (int i = 0; i < 1_00000; i++)
    {
        string id = i.ToString();
        books.Add(new Book { Name = id, Id = id });
    }

    var formatter = new BinaryFormatter();
    var mem = new MemoryStream();
    formatter.Serialize(mem, books);
    mem.Position = 0;

    formatter.Deserialize(mem);
}
```

### Regex:

Input data is taken from Roslyn source code: [CSharpUseDeconstructionDiagnosticAnalyzer.cs](https://github.com/dotnet/roslyn/blob/master/src/Analyzers/CSharp/Analyzers/UseDeconstruction/CSharpUseDeconstructionDiagnosticAnalyzer.cs)

#### Email:

```csharp
_regexEmail = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled);

 _regexEmail.IsMatch(_commonInput);
```

#### Strong password:

```csharp
_regexStrongPassword = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", RegexOptions.Compiled);

_regexStrongPassword.IsMatch(_commonInput);
```

#### Span-based searching with Vectorized Methods:

```csharp
_regexSpanSearching = new Regex("([ab]cd|ef[g-i])jklm", RegexOptions.Compiled);

_regexSpanSearching.IsMatch(_commonInput);
```

#### Backtracking elimination:

```csharp
_regexBackTracking = new Regex("a*a*a*a*a*a*a*b", RegexOptions.Compiled);;

_regexBackTracking.IsMatch("aaaaaaaaaaaaaaaaaaaaa");
```

## Summary
.NET Core is much, much faster than .NET Framework. New .NET 5 is similar to .NET Core 3.1x, but we can see vast improvements in regular expression engine:

Benchmark | RatioSummary | Notes
------------ | ------------------- | -------------
Enum | 2x | Improved Enum.Parse/TryParse
Linq | 8x | Linq optimizations, rewritten operators
SHA256 | 2-14x | Native cryptography in C++ (.NET Framework doesn't utilize AMD's cryptography features!) - CNG on Windows / OpenSSL on Unix. More about why and how AMD is faster you can find here: [Will AMD’s Ryzen finally bring SHA extensions to Intel’s CPUs?](https://neosmart.net/blog/2017/will-amds-ryzen-finally-bring-sha-extensions-to-intels-cpus/)
String | 2-3x | Improvements related to String/Char
Deserialize | 2-12x | Better deserialization performance on biggers objects
Regex |2-70k (!!)| Huge improvements in .NET 5 compared to .NET Core and .NET Framework: [Regex Performance Improvements in .NET 5](https://devblogs.microsoft.com/dotnet/regex-performance-improvements-in-net-5/)


### More test results from [https://github.com/plukawski](https://github.com/plukawski)
Blog post based on DotNetFrameworkVsCore: [.NET Core vs .NET Framework: Testing Performance])https://www.softwarehut.com/blog/tech/net-core-vs-net-framework-testing-performance)

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=5.0.100-preview.2.20176.6
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  Job-ZUTQCR : .NET Framework 4.8 (4.8.4150.0), X64 RyuJIT
  Job-ATUCIV : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Job-IFEMVP : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
```

|               Method |       Runtime |            Mean | Ratio |
|--------------------- |-------------- |----------------:|------:|
|          Regex_Email |      .NET 4.8 |  1,596,947.8 ns |  1.00 |
|          Regex_Email | .NET Core 3.1 |  1,356,909.3 ns |  0.85 |
|          Regex_Email | .NET Core 5.0 |    512,156.2 ns |  0.32 |
|                      |               |                 |       |
| Regex_StrongPassword |      .NET 4.8 |      1,237.2 ns |  1.00 |
| Regex_StrongPassword | .NET Core 3.1 |      1,039.0 ns |  0.84 |
| Regex_StrongPassword | .NET Core 5.0 |        276.6 ns |  0.22 |
|                      |               |                 |       |
|  Regex_SpanSearching |      .NET 4.8 |    252,325.8 ns |  1.00 |
|  Regex_SpanSearching | .NET Core 3.1 |    209,693.3 ns |  0.83 |
|  Regex_SpanSearching | .NET Core 5.0 |     15,778.5 ns |  0.06 |
|                      |               |                 |       |
|   Regex_BackTracking |      .NET 4.8 | 33,101,199.0 ns | 1.000 |
|   Regex_BackTracking | .NET Core 3.1 | 24,998,875.2 ns | 0.755 |
|   Regex_BackTracking | .NET Core 5.0 |        411.4 ns | 0.000 |
|                      |               |                 |       |
| EnumParse            | .NET 4.8      |           199.1 ns |  1.00 |
| EnumParse            | .NET Core 3.1 |           124.4 ns |  0.62 |
| EnumParse            | .NET Core 5.0 |           125.7 ns |  0.63 |
|                      |               |                    |       |
| LinqOrderBySkipFirst | .NET 4.8      | 1,291,386,100.0 ns |  1.00 |
| LinqOrderBySkipFirst | .NET Core 3.1 |   158,975,148.2 ns |  0.12 |
| LinqOrderBySkipFirst | .NET Core 5.0 |   158,977,596.4 ns |  0.12 |
|                      |               |                    |       |
| Sha256               | .NET 4.8      |   649,423,446.2 ns |  1.00 |
| Sha256               | .NET Core 3.1 |    50,023,144.0 ns |  0.08 |
| Sha256               | .NET Core 5.0 |    49,985,630.0 ns |  0.08 |
|                      |               |                    |       |
| StringStartsWith     | .NET 4.8      | 1,196,827,121.4 ns |  1.00 |
| StringStartsWith     | .NET Core 3.1 |   449,453,993.3 ns |  0.38 |
| StringStartsWith     | .NET Core 5.0 |   429,479,420.0 ns |  0.36 |
|                      |               |                    |       |
| Deserialize          | .NET 4.8      |   644,708,006.7 ns |  1.00 |
| Deserialize          | .NET Core 3.1 |   303,057,114.3 ns |  0.47 |
| Deserialize          | .NET Core 5.0 |   291,656,106.7 ns |  0.45 |

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
AMD EPYC 7452, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.100-preview.2.20176.6
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  Job-NFSGVR : .NET Framework 4.8 (4.8.4150.0), X64 RyuJIT
  Job-JDZARQ : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  Job-XMFFDJ : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
```

| Method               | Runtime       |            Mean | Ratio |
|----------------------|---------------|----------------:|------:|
| Regex_Email          | .NET 4.8      |  2,069,762.0 ns |  1.00 |
| Regex_Email          | .NET Core 3.1 |  1,696,688.2 ns |  0.82 |
| Regex_Email          | .NET Core 5.0 |    668,848.8 ns |  0.32 |
|                      |               |                 |       |
| Regex_StrongPassword | .NET 4.8      |      1,583.3 ns |  1.00 |
| Regex_StrongPassword | .NET Core 3.1 |      1,324.0 ns |  0.84 |
| Regex_StrongPassword | .NET Core 5.0 |        356.4 ns |  0.23 |
|                      |               |                 |       |
| Regex_SpanSearching  | .NET 4.8      |    325,314.0 ns |  1.00 |
| Regex_SpanSearching  | .NET Core 3.1 |    280,526.6 ns |  0.86 |
| Regex_SpanSearching  | .NET Core 5.0 |     20,132.1 ns |  0.06 |
|                      |               |                 |       |
| Regex_BackTracking   | .NET 4.8      | 42,860,065.0 ns | 1.000 |
| Regex_BackTracking   | .NET Core 3.1 | 31,944,158.3 ns | 0.745 |
| Regex_BackTracking   | .NET Core 5.0 |        540.0 ns | 0.000 |
|                      |               |                 |       |
| EnumParse            | .NET 4.8      |           257.6 ns |  1.00 |
| EnumParse            | .NET Core 3.1 |           173.9 ns |  0.67 |
| EnumParse            | .NET Core 5.0 |           160.4 ns |  0.62 |
|                      |               |                    |       |
| LinqOrderBySkipFirst | .NET 4.8      | 1,692,676,592.3 ns |  1.00 |
| LinqOrderBySkipFirst | .NET Core 3.1 |   210,773,388.9 ns |  0.12 |
| LinqOrderBySkipFirst | .NET Core 5.0 |   211,571,962.2 ns |  0.12 |
|                      |               |                    |       |
| Sha256               | .NET 4.8      |   838,048,820.0 ns |  1.00 |
| Sha256               | .NET Core 3.1 |    64,524,352.5 ns |  0.08 |
| Sha256               | .NET Core 5.0 |    64,530,346.7 ns |  0.08 |
|                      |               |                    |       |
| StringStartsWith     | .NET 4.8      | 1,589,253,135.3 ns |  1.00 |
| StringStartsWith     | .NET Core 3.1 |   547,359,884.6 ns |  0.34 |
| StringStartsWith     | .NET Core 5.0 |   608,590,760.0 ns |  0.38 |
|                      |               |                    |       |
| Deserialize          | .NET 4.8      |   984,847,792.9 ns |  1.00 |
| Deserialize          | .NET Core 3.1 |   423,896,185.7 ns |  0.43 |
| Deserialize          | .NET Core 5.0 |   414,115,746.7 ns |  0.42 |

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.100-preview.2.20176.6
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  Job-OFLLPY : .NET Framework 4.8 (4.8.4150.0), X64 RyuJIT
  Job-YMNQKZ : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  Job-YNHYXG : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  ```

| Method               | Runtime       |            Mean | Ratio |
|----------------------|---------------|----------------:|------:|
| Regex_Email          | .NET 4.8      |  2,499,773.9 ns |  1.00 |
| Regex_Email          | .NET Core 3.1 |  2,068,008.8 ns |  0.83 |
| Regex_Email          | .NET Core 5.0 |    819,443.4 ns |  0.33 |
|                      |               |                 |       |
| Regex_StrongPassword | .NET 4.8      |      2,073.3 ns |  1.00 |
| Regex_StrongPassword | .NET Core 3.1 |      1,831.4 ns |  0.89 |
| Regex_StrongPassword | .NET Core 5.0 |        466.2 ns |  0.22 |
|                      |               |                 |       |
| Regex_SpanSearching  | .NET 4.8      |    369,914.6 ns |  1.00 |
| Regex_SpanSearching  | .NET Core 3.1 |    343,073.6 ns |  0.92 |
| Regex_SpanSearching  | .NET Core 5.0 |     25,006.9 ns |  0.07 |
|                      |               |                 |       |
| Regex_BackTracking   | .NET 4.8      | 49,816,213.9 ns | 1.000 |
| Regex_BackTracking   | .NET Core 3.1 | 36,579,091.8 ns | 0.731 |
| Regex_BackTracking   | .NET Core 5.0 |        629.1 ns | 0.000 |
|                      |               |                    |       |
| EnumParse            | .NET 4.8      |           302.5 ns |  1.00 |
| EnumParse            | .NET Core 3.1 |           180.8 ns |  0.60 |
| EnumParse            | .NET Core 5.0 |           171.7 ns |  0.57 |
|                      |               |                    |       |
| LinqOrderBySkipFirst | .NET 4.8      | 1,995,979,569.2 ns |  1.00 |
| LinqOrderBySkipFirst | .NET Core 3.1 |   250,035,090.5 ns |  0.12 |
| LinqOrderBySkipFirst | .NET Core 5.0 |   260,239,916.2 ns |  0.13 |
|                      |               |                    |       |
| Sha256               | .NET 4.8      | 1,091,537,713.3 ns |  1.00 |
| Sha256               | .NET Core 3.1 |   529,427,276.5 ns |  0.48 |
| Sha256               | .NET Core 5.0 |   541,877,097.1 ns |  0.50 |
|                      |               |                    |       |
| StringStartsWith     | .NET 4.8      | 2,051,764,392.9 ns |  1.00 |
| StringStartsWith     | .NET Core 3.1 |   995,279,857.1 ns |  0.49 |
| StringStartsWith     | .NET Core 5.0 |   879,329,027.3 ns |  0.43 |
|                      |               |                    |       |
| Deserialize          | .NET 4.8      |   773,173,011.8 ns |  1.00 |
| Deserialize          | .NET Core 3.1 |   409,842,688.2 ns |  0.53 |
| Deserialize          | .NET Core 5.0 |   391,988,365.4 ns |  0.51 |

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Xeon Platinum 8171M CPU 2.60GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.100-preview.2.20176.6
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  Job-EXMOTN : .NET Framework 4.8 (4.8.4150.0), X64 RyuJIT
  Job-HBVQSG : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  Job-ZDNBKV : .NET Core 5.0.0 (CoreCLR 5.0.20.16006, CoreFX 5.0.20.16006), X64 RyuJIT
  ```

| Method               | Runtime       |            Mean | Ratio |
|----------------------|---------------|----------------:|------:|
| Regex_Email          | .NET 4.8      |  2,619,739.0 ns |  1.00 |
| Regex_Email          | .NET Core 3.1 |  2,159,228.2 ns |  0.82 |
| Regex_Email          | .NET Core 5.0 |    844,835.4 ns |  0.32 |
|                      |               |                 |       |
| Regex_StrongPassword | .NET 4.8      |      2,170.1 ns |  1.00 |
| Regex_StrongPassword | .NET Core 3.1 |      1,906.8 ns |  0.88 |
| Regex_StrongPassword | .NET Core 5.0 |        499.8 ns |  0.23 |
|                      |               |                 |       |
| Regex_SpanSearching  | .NET 4.8      |    405,482.9 ns |  1.00 |
| Regex_SpanSearching  | .NET Core 3.1 |    376,713.8 ns |  0.93 |
| Regex_SpanSearching  | .NET Core 5.0 |     26,474.7 ns |  0.07 |
|                      |               |                 |       |
| Regex_BackTracking   | .NET 4.8      | 49,737,000.6 ns | 1.000 |
| Regex_BackTracking   | .NET Core 3.1 | 39,805,924.7 ns | 0.802 |
| Regex_BackTracking   | .NET Core 5.0 |        678.5 ns | 0.000 |
|                      |               |                 |       |
| EnumParse            | .NET 4.8      |           343.1 ns |  1.00 |
| EnumParse            | .NET Core 3.1 |           188.5 ns |  0.55 |
| EnumParse            | .NET Core 5.0 |           188.7 ns |  0.55 |
|                      |               |                    |       |
| LinqOrderBySkipFirst | .NET 4.8      | 2,113,597,227.8 ns |  1.00 |
| LinqOrderBySkipFirst | .NET Core 3.1 |   274,652,566.7 ns |  0.13 |
| LinqOrderBySkipFirst | .NET Core 5.0 |   267,348,370.0 ns |  0.12 |
|                      |               |                    |       |
| Sha256               | .NET 4.8      | 1,222,152,246.7 ns |  1.00 |
| Sha256               | .NET Core 3.1 |   632,374,133.3 ns |  0.52 |
| Sha256               | .NET Core 5.0 |   634,169,393.3 ns |  0.52 |
|                      |               |                    |       |
| StringStartsWith     | .NET 4.8      | 2,197,562,306.7 ns |  1.00 |
| StringStartsWith     | .NET Core 3.1 |   868,539,978.6 ns |  0.40 |
| StringStartsWith     | .NET Core 5.0 |   863,221,673.3 ns |  0.39 |
|                      |               |                    |       |
| Deserialize          | .NET 4.8      | 1,231,583,513.6 ns |  1.00 |
| Deserialize          | .NET Core 3.1 |   474,690,685.7 ns |  0.39 |
| Deserialize          | .NET Core 5.0 |   457,693,543.8 ns |  0.37 |
