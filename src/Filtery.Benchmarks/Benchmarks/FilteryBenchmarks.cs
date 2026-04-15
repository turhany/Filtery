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
    public class FilteryBenchmarks
    {
        private List<User> _list;
        private IQueryable<User> _queryable;
        private UserFilteryMappings _mapping;

        [Params(100, 1_000, 10_000)]
        public int ItemCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _list = SampleData.Generate(ItemCount);
            _queryable = _list.AsQueryable();
            _mapping = new UserFilteryMappings();
        }

        private static FilteryRequest SingleAndFilter() => new FilteryRequest
        {
            PageNumber = 1,
            PageSize = 25,
            AndFilters = new List<FilterItem>
            {
                new FilterItem { TargetFieldName = "age", Operation = FilterOperation.GreaterThan, Value = 30 }
            },
            OrFilters = new List<FilterItem>(),
            OrderOperations = new Dictionary<string, OrderOperation>()
        };

        private static FilteryRequest MultipleAndFilter() => new FilteryRequest
        {
            PageNumber = 1,
            PageSize = 25,
            AndFilters = new List<FilterItem>
            {
                new FilterItem { TargetFieldName = "age", Operation = FilterOperation.GreaterThan, Value = 20 },
                new FilterItem { TargetFieldName = "age", Operation = FilterOperation.LessThan, Value = 60 },
                new FilterItem { TargetFieldName = "licence", Operation = FilterOperation.Equal, Value = true }
            },
            OrFilters = new List<FilterItem>(),
            OrderOperations = new Dictionary<string, OrderOperation>()
        };

        private static FilteryRequest AndOrFilterWithOrder() => new FilteryRequest
        {
            PageNumber = 2,
            PageSize = 25,
            AndFilters = new List<FilterItem>
            {
                new FilterItem { TargetFieldName = "age", Operation = FilterOperation.GreaterThanOrEqual, Value = 18 }
            },
            OrFilters = new List<FilterItem>
            {
                new FilterItem { TargetFieldName = "name", Operation = FilterOperation.StartsWith, Value = "A" },
                new FilterItem { TargetFieldName = "name", Operation = FilterOperation.StartsWith, Value = "J" }
            },
            OrderOperations = new Dictionary<string, OrderOperation>
            {
                { "age", OrderOperation.Descending },
                { "name", OrderOperation.Ascending }
            }
        };

        private static FilteryRequest StringContainsFilter() => new FilteryRequest
        {
            PageNumber = 1,
            PageSize = 25,
            AndFilters = new List<FilterItem>
            {
                new FilterItem { TargetFieldName = "name", Operation = FilterOperation.Contains, Value = "a" }
            },
            OrFilters = new List<FilterItem>(),
            OrderOperations = new Dictionary<string, OrderOperation>()
        };

        // ---------------- IEnumerable path ----------------

        [Benchmark]
        [BenchmarkCategory("IEnumerable")]
        public FilteryResponse<User> Enumerable_SingleFilter()
            => _list.BuildFiltery(_mapping, SingleAndFilter());

        [Benchmark]
        [BenchmarkCategory("IEnumerable")]
        public FilteryResponse<User> Enumerable_MultipleFilter()
            => _list.BuildFiltery(_mapping, MultipleAndFilter());

        [Benchmark]
        [BenchmarkCategory("IEnumerable")]
        public FilteryResponse<User> Enumerable_AndOrFilterWithOrder()
            => _list.BuildFiltery(_mapping, AndOrFilterWithOrder());

        [Benchmark]
        [BenchmarkCategory("IEnumerable")]
        public FilteryResponse<User> Enumerable_StringContains()
            => _list.BuildFiltery(_mapping, StringContainsFilter());

        // ---------------- IQueryable path ----------------

        [Benchmark]
        [BenchmarkCategory("IQueryable")]
        public FilteryResponse<User> Queryable_SingleFilter()
            => _queryable.BuildFiltery(_mapping, SingleAndFilter());

        [Benchmark]
        [BenchmarkCategory("IQueryable")]
        public FilteryResponse<User> Queryable_MultipleFilter()
            => _queryable.BuildFiltery(_mapping, MultipleAndFilter());

        [Benchmark]
        [BenchmarkCategory("IQueryable")]
        public FilteryResponse<User> Queryable_AndOrFilterWithOrder()
            => _queryable.BuildFiltery(_mapping, AndOrFilterWithOrder());

        [Benchmark]
        [BenchmarkCategory("IQueryable")]
        public FilteryResponse<User> Queryable_StringContains()
            => _queryable.BuildFiltery(_mapping, StringContainsFilter());
    }
}
