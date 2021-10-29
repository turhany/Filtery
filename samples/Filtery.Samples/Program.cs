﻿using System;
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
                FirstName = "Türhan", 
                LastName = "Yıldırım", 
                Age = 22, 
                HasDriverLicence = true, 
                Birthdate = new DateTime(1987, 06, 06), 
                Address = new Address{Country = "Bulgaristan", City = "Şumen"},
                ParentNames = new List<string>{ "Recep", "Fatma" }
            });
            userList.Add(new User
            {
                FirstName = "Çağla", 
                LastName = "Yıldırım", 
                Age = 18, 
                HasDriverLicence = true, 
                Birthdate = new DateTime(1997, 09, 27), Address = new Address{Country = "Türkiye", City = "İstanbul"},
                ParentNames = new List<string>{ "Turgut" }
            });

            var filteryQuery = new FilteryRequest
            {
                //String
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "name", Value = "türhan", Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "name", Value = "çağla", Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "name", Value = "türhan", Operation = FilterOperation.Contains},
                //     new FilterItem {TargetFieldName = "name", Value = "türhan", Operation = FilterOperation.StartsWith},
                //     new FilterItem {TargetFieldName = "name", Value = "türhan", Operation = FilterOperation.EndsWith}
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
                
                //Navigation String List
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "parentnames", Value = "Fatma", Operation = FilterOperation.Contains}
                },
                
                OrderOperations = new Dictionary<string, OrderOperation>
                {
                    {"name", OrderOperation.Ascending}
                },
                PageNumber = 1,
                PageSize = 2
            };
            
            var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery).ToList();

            Console.ReadKey();
        }
    }
}