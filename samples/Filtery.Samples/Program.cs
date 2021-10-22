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
                RegisterMappingsFromAssembly = typeof(UserFilteryMappings).Assembly
            });
            
            var userList = new List<User>();
            userList.Add(new User{FirstName = "Türhan", LastName = "Yıldırım", Age = 22});
            userList.Add(new User{FirstName = "Çağla", LastName = "Yıldırım", Age = 18});

            var response = userList.BuildFiltery(new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem{TargetFieldName = "name", Value = "Ça",Operation = FilterOperation.Contains},              
                    new FilterItem{TargetFieldName = "last", Value = "Yıl",Operation = FilterOperation.Contains}                
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