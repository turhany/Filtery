using System;
using System.Collections.Generic;
#pragma warning disable 8618

namespace Filtery.Tests.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool HasDriverLicence { get; set; }
        public DateTime Birthdate { get; set; }
        public Address Address { get; set; }
        public List<string> ParentNames { get; set; }
    }
}