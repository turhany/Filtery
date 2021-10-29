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
                
                 AndFilters = new List<FilterItem>
                 {
                     new FilterItem {TargetFieldName = "name", Value = "Çağla", Operation = FilterOperation.Equal},
                     //new FilterItem {TargetFieldName = "parentsnamecontains", Value = "Fatma"}, //Not work
                     //new FilterItem {TargetFieldName = "parentsname", Value = "Fatma", Operation = FilterOperation.Contains}, //Not work
                     //new FilterItem {TargetFieldName = "ages", Value = 20, Operation = FilterOperation.Contains}, //Not work
                 },
                
                //String
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.Contains},
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.GreaterThan}, //Not work
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.LessThan}, //Not work
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.GreaterThanOrEqual}, //Not work
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.LessThanOrEqual}, //Not work
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.StartsWith},
                //     new FilterItem {TargetFieldName = "name", Value = "test", Operation = FilterOperation.EndsWith}
                // },
                
                //integer
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "age", Value = 18, Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.Contains}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.GreaterThan},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.LessThan},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.GreaterThanOrEqual},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.LessThanOrEqual},
                //     new FilterItem {TargetFieldName = "age", Value = 3, Operation = FilterOperation.StartsWith}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "age", Value = 8, Operation = FilterOperation.EndsWith} //Converted to Equal
                // },
                
                //datetime
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "date", Value = new DateTime(1987, 06, 06), Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.Contains}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.GreaterThan},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.LessThan},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.GreaterThanOrEqual},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.LessThanOrEqual},
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.StartsWith}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "date", Value = DateTime.Now, Operation = FilterOperation.EndsWith} //Converted to Equal
                // },
                
                //bool
                // AndFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "licence", Value = false, Operation = FilterOperation.Equal},
                //     new FilterItem {TargetFieldName = "licence", Value = false, Operation = FilterOperation.NotEqual},
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.Contains}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.GreaterThan}, //Not work
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.LessThan}, // Not work
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.GreaterThanOrEqual}, // Not work
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.LessThanOrEqual}, // Not work
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.StartsWith}, //Converted to Equal
                //     new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.EndsWith} //Converted to Equal
                // },
                
                // OrFilters = new List<FilterItem>
                // {
                //     new FilterItem {TargetFieldName = "name", Value = "ça", Operation = FilterOperation.Contains },
                //     new FilterItem {TargetFieldName = "last", Value = "Yıl", Operation = FilterOperation.Contains, CaseSensitive = true}
                // },
                
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