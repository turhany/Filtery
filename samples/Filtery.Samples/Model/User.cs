using System;
using System.Collections.Generic;

namespace Filtery.Samples.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool HasDriverLicence { get; set; }
        public DateTime Birthdate { get; set; }
        public Address Address { get; set; }
        public List<string> ParentNames { get; set; }
    }
}