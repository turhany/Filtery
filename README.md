#   **Filtery**

![alt tag](https://raw.githubusercontent.com/turhany/Filtery/main/img/monolithicsync.png)  

Simple and Extensible Filtering, Sorting and Paging  library.

[![NuGet version](https://badge.fury.io/nu/Filtery.svg)](https://badge.fury.io/nu/Filtery)  ![Nuget](https://img.shields.io/nuget/dt/Filtery)

#### Features:
- MVC Service Collection registeration support
- Custom filter "Key" and "Property" mapping for search and order
- Also support paging

#### Supported Filter Operations:
- Equal
- NotEqual
- Contains
- GreaterThan
- LowerThan
- GreaterThanAndEqual
- LowerThanAndEqual
- Include
- Between

#### Supported Order Operations:
- Asc (Ascending)
- Desc (Descending)

#### Usages:
DI Registration:

```cs
    public void ConfigureServices(IServiceCollection services)
    {
        service.AddFilteryConfiguration(new FilteryConfiguration
        {
            DefaultPageSize = 10,
            RegisterMappingsFromAssembly = typeof(UserFilteryMappings).Assembly
        });
    }
```

Sample Model:

```cs
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
```

Filter Mapping File:

```cs
    public class UserFilteryMappings : IFilteryMapping<User>
    {
        public void FilteryMappings(FilteryMapper<User> mapper)
        {
            mapper.Name("name").Property(p => p.FirstName);
            mapper.Name("last").Property(p => p.LastName);
        }
    }
```

Usage:

```cs
    var userList = new List<User>();
    userList.Add(new User{FirstName = "Türhan", LastName = "Yıldırım", Age = 22});
    userList.Add(new User{FirstName = "Çağla", LastName = "Yıldırım", Age = 18});

    var response = userList.BuildFiltery(new FilteryRequest
    {
        AndFilters = new List<FilterItem>
        {
            new FilterItem{TargetFieldName = "name", Value = "Ça",Operation = FilterOperation.Contains},              
            new FilterItem{TargetFieldName = "last", Value = "Yıl",Operation = FilterOperation.Contains}                
        },
        OrderOperations = new Dictionary<string, OrderOperation>()
        {
            {"name", OrderOperation.Asc}
        },
        PageNumber = 1,
        PageSize = 2
    });
```

### Release Notes

#### 1.0.0
* Broken releases
