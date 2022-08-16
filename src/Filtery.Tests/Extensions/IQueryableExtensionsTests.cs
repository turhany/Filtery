using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filtery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filtery.Models;
using Filtery.Models.Filter;
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "Alisa");
            Assert.AreEqual(response.Data.First().Age, 18);
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "Alisa");
            Assert.AreEqual(response.Data.First().Age, 18);
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Age, 22);
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Age, 18);
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
            Assert.AreEqual(response.TotalItemCount, 2);
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
            Assert.AreEqual(response.TotalItemCount, 2);
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1987, 06, 06)); 
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1987, 06, 06)); 
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1997, 09, 27)); 
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1987, 06, 06)); 
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1997, 09, 27)); 
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().Birthdate, new DateTime(1987, 06, 06)); 
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
            Assert.AreEqual(response.TotalItemCount, 2);
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
            Assert.AreEqual(response.TotalItemCount, 0);
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 1);
            Assert.AreEqual(response.Data.First().FirstName, "John");
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
            Assert.AreEqual(response.TotalItemCount, 2);
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
            Assert.AreEqual(response.TotalItemCount, 2);
        }
    }
}