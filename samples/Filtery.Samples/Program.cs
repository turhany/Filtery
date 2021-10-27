using System;
using System.Collections.Generic; 
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;
using Filtery.Samples.Mappings;
using Filtery.Samples.Model; 

namespace Filtery.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var userList = new List<User>();
            userList.Add(new User{FirstName = "Türhan", LastName = "Yıldırım", Age = 22});
            userList.Add(new User{FirstName = "Çağla", LastName = "Yıldırım", Age = 18});

            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "name", Value = "ça", Operation = FilterOperation.Contains, CaseSensitive = false},
                    new FilterItem {TargetFieldName = "last", Value = "Yıl", Operation = FilterOperation.Contains}
                },
                OrderOperations = new Dictionary<string, OrderOperation>()
                {
                    {"name", OrderOperation.Asc}
                },
                PageNumber = 1,
                PageSize = 2
            };
            
            var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery);
            
            Console.WriteLine("Hello World!");
        }
    }
}