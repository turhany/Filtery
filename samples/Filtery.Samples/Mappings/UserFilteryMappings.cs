using Filtery.Configuration.Filtery;
using Filtery.Samples.Model;

namespace Filtery.Samples.Mappings
{
    public class UserFilteryMappings : AbstractFilteryMapping<User>
    {
        public UserFilteryMappings()
        {
            mapper.Name("name").Property(p => p.FirstName);
            mapper.Name("last").Property(p => p.LastName);
            mapper.Name("age").Property(p => p.Age);
            mapper.Name("date").Property(p => p.Birthdate);
            mapper.Name("licence").Property(p => p.HasDriverLicence);
            mapper.Name("country").Property(p => p.Address.Country);
        }
    }
}