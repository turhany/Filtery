using System;
using System.Collections.Generic;
using System.Linq;
using Filtery.Tests.Model;

namespace Filtery.Tests
{
    public class TestBase
    {
        public List<User> SampleList { get; set; } = new List<User>();
        public IQueryable<User> SampleQueryableList { get; set; }
        
        public TestBase()
        { 
            SampleList.Add(new User
            {
                FirstName = "John", 
                LastName = "Doe", 
                Age = 22, 
                HasDriverLicence = true, 
                Birthdate = new DateTime(1987, 06, 06), 
                Address = new Address{Country = "Netherland", City = "Amsterdam"},
                ParentNames = new List<string>{ "Bob", "Sera" }
            });
            SampleList.Add(new User
            {
                FirstName = "Alisa", 
                LastName = "Doe", 
                Age = 18, 
                HasDriverLicence = true, 
                Birthdate = new DateTime(1997, 09, 27), 
                Address = new Address{Country = "Mexico", City = "Merida"},
                ParentNames = new List<string>{ "Fernando", "Elena" }
            });

            SampleQueryableList = SampleList.AsQueryable();
        }
    }
}