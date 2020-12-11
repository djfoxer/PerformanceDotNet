# DotNetFrameworkVsCore - compare multiple .NET runtimes on various OS
Small console application to compare performance of **.NET Framework (4.8), .NET Core (3.1.x), .NET 5 and Mono (6.8.0)**.

Common tests are written in .NET Standard. App contains 5 benchmarks to test performance of .NET Framework (4.8), .NET Core (3.1.x), .NET 5 and Mono (6.8.0). Another 4 benchmarks are related to regex performance. Results taken by [BenchmarkDotNet](https://benchmarkdotnet.org/).

 You can easily  check how fast/slow is .NET Framework/.NET Core/.NET 5/Mono. But we can do more. .NET Core, .NET 5 and Mono are **multi-platform runtimes**. It's very easy now to compare various .NET platform on **Windows and Linux**.

**Try .NET 5** - .NET 5 is fresh, shiny and ready to test. To check new runtime you have to download [Visual Studio 2019 16.6.0+](https://visualstudio.microsoft.com/en/vs/) and [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0). In this release .NET Team was focused to improve regex engine.

# Code details

DotNetFrameworkVsCore contains multiple test to compare .NET performance. Each of method can be run on .NET Framework, .NET Core, .NET 5,  Mono and are fully compatible with Windows and Linux (should also run on MacOS).

### Enum:
```csharp
public DayOfWeek EnumParse() => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Thursday");
```

### Linq:
```csharp
//IEnumerable<int> _tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();

public int LinqOrderBySkipFirst() => _tenMillionToZero.OrderBy(i => i).Skip(4).First();
```

### SHA256:
```csharp
//byte[] _raw = new byte[100 * 1024 * 1024];
//for (int index = 0; index < _raw.Length; index++) _raw[index] = (byte)index;

public byte[] Sha256() => _sha256.ComputeHash(_raw);
```

### String:
```csharp
// static string _s = "abcdefghijklmnopqrstuvwxyz";

public bool StringStartsWith()
{
    var data = false;
    for (int i = 0; i < 100_000_000; i++)
    {
        data = _s.StartsWith("abcdefghijklmnopqrstuvwxy-", StringComparison.Ordinal);
    }
    return data;
}
```

### Deserialize:
```csharp

//var _books = new List<Book>();
//for (int i = 0; i < 1_00000; i++)
//{
//    string id = i.ToString();
//    _books.Add(new Book { Name = id, Id = id });
//}
    
public object Deserialize()
{    
    var formatter = new BinaryFormatter();
    var mem = new MemoryStream();
    formatter.Serialize(mem, _books);
    mem.Position = 0;

    return formatter.Deserialize(mem);
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


# Benchmark experiment  #1: 
## .NET Framework (4.8) vs .NET Core (3.1.x) vs .NET 5 (preview 2) on Windows 10 (and Intel vs AMD)

![Chart](img/ben1.png)

In first benchmark .NET Framework, .NET Core and .NET 5 were compared on Windows 10. Results, charts and more details with summary you can find here **[Benchmark #1: NET Framework (4.8) vs .NET Core (3.1.x) vs .NET 5 (preview 2) on Windows 10](doc/Benchmark_Windows_ClassicCoreNet5.md)**

* Blog post about performance of multiple .NET runtimes on Windows 10 (and few details about differences between Intel and AMD) - [.NET Core is dead, long live the .NET Core (and .NET 5) and its performance](https://www.dobreprogramy.pl/djfoxer/Umarl-NET-Framework-niech-zyje-NET-Core-oraz-NET-i-jego-wydajnosc,105443.html) (only in Polish!)
* Blog post based on DotNetFrameworkVsCore: [.NET Core vs .NET Framework: Testing Performance](https://www.softwarehut.com/blog/tech/net-core-vs-net-framework-testing-performance)

# Benchmark experiment  #2: 
## Linux (Ubuntu 20.04) vs Windows 10: .NET Core (3.1.x), .NET 5 (preview 3) and Mono (6.8.0)

![Chart](img/ben2.png)

In this benchmark multi-platform .NET runtimes were compared on Windows 10 and Linux (Ubunru 20.04). .NET Core, .NET 5 and Mono were taken to tests. Results, charts and more details with summary you can find here **[Benchmark #2: Linux (Ubuntu 20.04) vs Windows 10 and multi-platform .NET (.NET Core, .NET 5 and Mono)](doc/Benchmark_WindowsLinux_CoreNet5Mono.md)**

* Blog post about performance of multiple .NET runtimes on Windows and Linux - [.NET Linux vs Windows - performance benchmark  of .NET Core 3.1, .NET 5.0 and also Mono](https://www.dobreprogramy.pl/djfoxer/NET-Linux-vs-Windows-test-wydajnosci-NET-Core-NET-a-takze-Mono,107926.html) (only in Polish!)







