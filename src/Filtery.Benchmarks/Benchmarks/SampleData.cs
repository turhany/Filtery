using System;
using System.Collections.Generic;
using Filtery.Benchmarks.Model;

namespace Filtery.Benchmarks.Benchmarks
{
    internal static class SampleData
    {
        private static readonly string[] FirstNames =
        {
            "John", "Alisa", "Bob", "Sera", "Fernando", "Elena",
            "Mike", "Anna", "Chris", "Diana", "Ethan", "Fiona"
        };

        public static List<User> Generate(int count, int seed = 42)
        {
            var rng = new Random(seed);
            var list = new List<User>(count);
            for (var i = 0; i < count; i++)
            {
                var first = FirstNames[rng.Next(FirstNames.Length)];
                list.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = first,
                    LastName = "Doe" + i,
                    Age = rng.Next(1, 90),
                    HasDriverLicence = (i & 1) == 0,
                    Birthdate = new DateTime(1950, 1, 1).AddDays(rng.Next(0, 25000)),
                    Address = new Address { Country = "Country" + (i % 10), City = "City" + (i % 50) },
                    ParentNames = new List<string> { "P1_" + i, "P2_" + i }
                });
            }

            return list;
        }
    }
}
