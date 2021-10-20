using System;
using System.Collections.Generic;
using Filtery.Configuration.Startup;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;
using Filtery.Samples.Mappings;
using Filtery.Samples.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Filtery.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection service = null;

            service.AddFilteryConfiguration(new FilteryConfiguration
            {
                DefaultPageSize = 10,
                RegisterMappingsFromAssemblyContaining = typeof(UserFilteryMappings)
            });
            
            var userList = new List<User>();

            var response = userList.BuildFiltery(new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem{TargetFieldName = "name", Value = "tuy",Operation = FilterOperation.Contains},              
                    new FilterItem{TargetFieldName = "last", Value = "yıl",Operation = FilterOperation.Contains}                
                },
                OrderOperations = new Dictionary<string, OrderOperation>()
                {
                    {"name", OrderOperation.Asc}
                },
                PageNumber = 1,
                PageSize = 2
            });
            
            Console.WriteLine("Hello World!");
        }
    }
}