using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filtery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;
using Filtery.Tests;
using Filtery.Tests.Mappings;
using Filtery.Tests.Model;

namespace Filtery.Extensions.Tests
{
    [TestClass()]
    public class IQueryableExtensionsTests: TestBase
    {
        [TestMethod()]
        public void BuildFiltery_Equal_String()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Equal}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public void BuildFiltery_NotEqual_String()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {  
                    new FilterItem {TargetFieldName = "name", Value = "alisa", Operation = FilterOperation.NotEqual} 
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public void BuildFiltery_Contains_String()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {   
                    new FilterItem {TargetFieldName = "name", Value = "joh", Operation = FilterOperation.Contains} 
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public void BuildFiltery_StartsWith_String()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {   
                    new FilterItem {TargetFieldName = "name", Value = "jo", Operation = FilterOperation.StartsWith} 
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public void BuildFiltery_EndsWith_String()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {   
                    new FilterItem {TargetFieldName = "name", Value = "hn", Operation = FilterOperation.EndsWith}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public void BuildFiltery_Equal_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 18, Operation = FilterOperation.Equal}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("Alisa", response.Data.First().FirstName);
            Assert.AreEqual(18, response.Data.First().Age);
        }
        
        [TestMethod()]
        public void BuildFiltery_NotEqual_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.NotEqual} 
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("Alisa", response.Data.First().FirstName);
            Assert.AreEqual(18, response.Data.First().Age);
        }
        
        [TestMethod()]
        public void BuildFiltery_GreaterThan_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 18, Operation = FilterOperation.GreaterThan}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(22, response.Data.First().Age);
        }
        
        [TestMethod()]
        public void BuildFiltery_LessThan_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.LessThan}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(18, response.Data.First().Age);
        }
        
        [TestMethod()]
        public void BuildFiltery_GreaterThanOrEqual_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 6, Operation = FilterOperation.GreaterThanOrEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
        }
        
        [TestMethod()]
        public void BuildFiltery_LessThanOrEqual_Integer()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "age", Value = 22, Operation = FilterOperation.LessThanOrEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
        }
        
        [TestMethod()]
        public void BuildFiltery_Equal_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1987, 06, 06), Operation = FilterOperation.Equal}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1987, 06, 06), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_NotEqual_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1997, 09, 27), Operation = FilterOperation.NotEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1987, 06, 06), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_GreaterThan_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1987, 06, 06), Operation = FilterOperation.GreaterThan}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1997, 09, 27), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_LessThan_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1997, 09, 27), Operation = FilterOperation.LessThan}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1987, 06, 06), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_GreaterThanOrEqual_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1990, 06, 06), Operation = FilterOperation.GreaterThanOrEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1997, 09, 27), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_LessThanOrEqual_DateTime()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "date", Value = new DateTime(1990, 06, 06), Operation = FilterOperation.LessThanOrEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual(new DateTime(1987, 06, 06), response.Data.First().Birthdate); 
        }
        
        [TestMethod()]
        public void BuildFiltery_Equal_Bool()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.Equal}
                    //     new FilterItem {TargetFieldName = "licence", Value = false, Operation = FilterOperation.NotEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
        }
        
        [TestMethod()]
        public void BuildFiltery_NotEqual_Bool()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "licence", Value = true, Operation = FilterOperation.NotEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(0, response.TotalItemCount);
        }
        
        [TestMethod()]
        public void BuildFiltery_Navigation_Property_Contains()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "parentnames", Value = "Sera", Operation = FilterOperation.Contains}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }
        
        [TestMethod()]
        public async Task BuildFiltery_Equal_Async()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "name", Value = "john", Operation = FilterOperation.Equal}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = await SampleQueryableList.BuildFilteryAsync(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(1, response.TotalItemCount);
            Assert.AreEqual("John", response.Data.First().FirstName);
        }

        [TestMethod()]
        public void BuildFiltery_Equal_Guid()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "id", Value = SampleQueryableList.First().Id, Operation = FilterOperation.Equal}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
        }

        [TestMethod()]
        public void BuildFiltery_NotEqual_Guid()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "id", Value = Guid.NewGuid(), Operation = FilterOperation.NotEqual}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
        }
        
        [TestMethod()]
        public void BuildFiltery_OrderBy_ASC()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "id", Value = Guid.NewGuid(), Operation = FilterOperation.NotEqual}
                },
                OrderOperations = new Dictionary<string, OrderOperation>()
                {
                    {"age", OrderOperation.Ascending}  
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(SampleQueryableList.OrderBy(p => p.Age).First().Age, response.Data.First().Age);
        }
        
        [TestMethod()]
        public void BuildFiltery_OrderBy_DESC()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem {TargetFieldName = "id", Value = Guid.NewGuid(), Operation = FilterOperation.NotEqual}
                },
                OrderOperations = new Dictionary<string, OrderOperation>()
                {
                    {"age", OrderOperation.Descending}
                },
                PageNumber = 1,
                PageSize = 2
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(SampleQueryableList.OrderByDescending(p => p.Age).First().Age, response.Data.First().Age);
        }

        [TestMethod()]
        public void BuildFiltery_MultiKey_OrderBy_Preserves_SecondarySort()
        {
            //arrange — 3 users: two with the same Age=18, differentiated by name
            var list = new List<User>
            {
                new User { FirstName = "Zara",  Age = 18, HasDriverLicence = true, Birthdate = DateTime.Now, Id = Guid.NewGuid(), ParentNames = new List<string>() },
                new User { FirstName = "Alice", Age = 18, HasDriverLicence = true, Birthdate = DateTime.Now, Id = Guid.NewGuid(), ParentNames = new List<string>() },
                new User { FirstName = "Bob",   Age = 25, HasDriverLicence = true, Birthdate = DateTime.Now, Id = Guid.NewGuid(), ParentNames = new List<string>() },
            }.AsQueryable();

            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Value = 0, Operation = FilterOperation.GreaterThanOrEqual }
                },
                OrderOperations = new Dictionary<string, OrderOperation>
                {
                    { "age",  OrderOperation.Ascending },
                    { "name", OrderOperation.Ascending }
                },
                PageNumber = 1,
                PageSize = 10
            };

            //act
            FilteryResponse<User> response = list.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert — Age=18 group: Alice should come before Zara (ascending by name)
            var expected = list.OrderBy(p => p.Age).ThenBy(p => p.FirstName).First();
            Assert.AreEqual(expected.FirstName, response.Data.First().FirstName);
        }

        [TestMethod()]
        public void BuildFiltery_NegativePageSize_FallsBackToDefault()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Value = 0, Operation = FilterOperation.GreaterThanOrEqual }
                },
                PageNumber = 1,
                PageSize = -5
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert — should return data using default page size, not empty or crash
            Assert.AreEqual(2, response.TotalItemCount);
            Assert.IsTrue(response.Data.Count > 0);
        }

        [TestMethod()]
        public void BuildFiltery_ZeroPageSize_FallsBackToDefault()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Value = 0, Operation = FilterOperation.GreaterThanOrEqual }
                },
                PageNumber = 1,
                PageSize = 0
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert — should return data using default page size, not empty
            Assert.AreEqual(2, response.TotalItemCount);
            Assert.IsTrue(response.Data.Count > 0);
        }

        [TestMethod()]
        public void BuildFiltery_TotalPageCount_CorrectCeilingDivision()
        {
            //arrange — 2 items, pageSize=1 → 2 pages
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Value = 0, Operation = FilterOperation.GreaterThanOrEqual }
                },
                PageNumber = 1,
                PageSize = 1
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert
            Assert.AreEqual(2, response.TotalItemCount);
            Assert.AreEqual(2, response.TotalPageCount);
        }

        [TestMethod()]
        public void BuildFiltery_ZeroPageNumber_FallsBackToFirstPage()
        {
            //arrange
            var filteryQuery = new FilteryRequest
            {
                AndFilters = new List<FilterItem>
                {
                    new FilterItem { TargetFieldName = "age", Value = 0, Operation = FilterOperation.GreaterThanOrEqual }
                },
                PageNumber = 0,
                PageSize = 1
            };

            //act
            FilteryResponse<User> response = SampleQueryableList.BuildFiltery(new UserFilteryMappings(), filteryQuery);

            //assert — page 0 treated as page 1, returns first item
            Assert.AreEqual(1, response.Data.Count);
        }
    }
}