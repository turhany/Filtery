using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Filtery.Benchmarks.Mappings;
using Filtery.Benchmarks.Model;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;

namespace Filtery.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net90)]
    public class HotPathMicroBenchmarks
    {
        private List<User> _list;
        private UserFilteryMappings _mapping;
        private FilteryRequest _request;

        [GlobalSetup]
        public void Setup()
        {
            _list = SampleData.Generate(1_000);
            _mapping = new UserFilteryMappings();
            _request = new FilteryRequest
            {
                PageNumber = 1,
                PageSize = 10,
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Operation = FilterOperation.GreaterThan, Value = 25 }
                },
                OrFilters = new List<FilterItem>(),
                OrderOperations = new Dictionary<string, OrderOperation>
                {
                    { "age", OrderOperation.Ascending }
                }
            };
        }

        [Benchmark(Description = "Full pipeline (parse + compile + filter + order + page)")]
        public FilteryResponse<User> FullPipeline()
            => _list.BuildFiltery(_mapping, _request);

        [Benchmark(Description = "Query only (no pagination materialize)")]
        public int QueryOnly()
            => _list.BuildFilteryQuery(_mapping, _request).Count();
    }
}
