using Filtery.Configuration.Filtery;
using Filtery.Samples.Model;

namespace Filtery.Samples.Mappings
{
    public class UserFilteryMappings : IFilteryMapping<User>
    {
        public void FilteryMappings(FilteryMapper<User> mapper)
        {
            mapper.Name("name").Property(p => p.FirstName);
            mapper.Name("last").Property(p => p.LastName);
        }
    }
}