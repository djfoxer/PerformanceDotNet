﻿using BenchmarkDotNet.Attributes;
using djfoxer.PerformanceDotNet.App.Benchmark.Helpers;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace djfoxer.PerformanceDotNet.App.Benchmark
{
    public class RegexBenchmark : BaseBenchmark
    {
        string _commonInput = string.Empty;
        Regex _regexEmail, _regexStrongPassword, _regexSpanSearching, _regexBackTracking;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 550; i++)
            {
                sb.Append(Guid.NewGuid());
                sb.Append("        ");
            }
            _commonInput = sb.ToString();

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
