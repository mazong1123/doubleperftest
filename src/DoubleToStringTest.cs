using System;
using Xunit;
using Microsoft.Xunit.Performance;
using Microsoft.Xunit.Performance.Api;
using System.Reflection;
using System.Globalization;

namespace perftest
{
    public class DoubleToStringTest
    {
        public static void Main(string[] args)
        {
            using (XunitPerformanceHarness h = new XunitPerformanceHarness(args))
            {
                string entryAssemblyPath = Assembly.GetEntryAssembly().Location;
                h.RunBenchmarks(entryAssemblyPath);
            }
        }

        [Benchmark(InnerIterationCount = 2000)]
        [InlineData("en", 104234.343)]
        [InlineData("en", double.MinValue/2)]
        [InlineData("en", Math.PI)]
        [InlineData("en", Math.E)]
        [InlineData("en", double.MaxValue)]
        [InlineData("en", double.MinValue)]
        [InlineData("en", double.NaN)]
        [InlineData("en", double.PositiveInfinity)]
        [InlineData("en", double.NegativeInfinity)]
        [InlineData("en", 0xFFFFFFFFFFFFF)]
        [InlineData("en", 0x800FFFFFFFFFFFFF)]
        [InlineData("en", 0x7FFFFFFFFFFFF)]
        [InlineData("en", 0x8000000000000000)]
        [InlineData("en", 0x0000000000000000)]
        public void DefaultToString(string cultureName, double number)
        {
            CultureInfo cultureInfo = new CultureInfo(cultureName);
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        number.ToString(cultureInfo);
                    }
                }
            }
        }
    }
}
