using System;
using System.Collections.Generic;
using System.Linq; 
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
            userList.Add(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John", 
                LastName = "Doe", 
                Age = 22, 
                Sex = Sex.Male,
                HasDriverLicence = true, 
                Birthdate = new DateTime(1987, 06, 06), 
                Address = new Address{Country = "Netherland", City = "Amsterdam"},
                ParentNames = new List<string>{ "Bob", "Sera" }
            });
            userList.Add(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Alisa", 
                LastName = "Doe", 
                Age = 18, 
                Sex = Sex.Female,
                HasDriverLicence = true, 
                Birthdate = new DateTime(1997, 09, 27), 
                Address = new Address{Country = "Mexico", City = "Merida"},
                ParentNames = new List<string>{ "Fernando", "Elena" }
            });

            var filteryQuery = new FilteryRequest
            {
                //string
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "name", Value = "alisa", Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Contains},
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.StartsWith},
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.EndsWith}
                // },

                //integer
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "age", Value = 18, Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "age", Value = 6, Operation = FilterOperation.GreaterThan},
                //     new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.LessThan},
                //     new FilterItem {TargetFieldName = "age", Value = 6, Operation = FilterOperation.GreaterThanOrEqual},
                //     new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.LessThanOrEqual}
                // },

                //datetime
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "date", Value = new DateTime(1987, 06, 06), Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "date", Value = new DateTime(1980, 06, 06), Operation = FilterOperation.GreaterThan},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.LessThan},
                //     new FilterItem {TargetFieldName = "date", Value = new DateTime(1980, 06, 06), Operation = FilterOperation.GreaterThanOrEqual},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.LessThanOrEqual}
                // },

                //bool
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "licence", Value = false, Operation = FilterOperation.NotEqual}
                // },

                //Navigation Property String List
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Equal},
                // },
                // OrFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "parentnames", Value = "Sera", Operation = FilterOperation.Contains},
                //     new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Equal},
                // },

                //Enum
                //AndFilters = new List<FilterItem>
                //{
                //    new FilterItem {TargetFieldName = "sex", Value = 1, Operation = FilterOperation.Equal}
                //},

                //Guid
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "id", Value = userList.First().Id, Operation = FilterOperation.Equal}
                },

                OrderOperations = new Dictionary<string, OrderOperation>
                {
                    {"name", OrderOperation.Ascending}
                },
                PageNumber = 1,
                PageSize = 2
            };
            
            var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery);
            Console.WriteLine(response.PageNumber);
            Console.WriteLine(response.PageSize);
            Console.WriteLine(response.TotalPageCount);
            
            var responseQueryable = userList.AsQueryable().BuildFiltery(new UserFilteryMappings(), filteryQuery);
            Console.WriteLine(responseQueryable.PageNumber);
            Console.WriteLine(responseQueryable.PageSize);
            Console.WriteLine(responseQueryable.TotalPageCount);
            
            Console.ReadKey();
        }
    }
}