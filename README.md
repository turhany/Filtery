#   **Filtery**

![alt tag](/img/filtery.png)  

Simple Filtering, Sorting and Paging  library.

[![NuGet version](https://badge.fury.io/nu/Filtery.svg)](https://badge.fury.io/nu/Filtery)  ![Nuget](https://img.shields.io/nuget/dt/Filtery)

#### Features:
- Custom filter "Key" and "Property" mapping for search and order
- Can manage string query CaseSensitive operation (Default is false)
- Also support paging (Default Page Number = 1, Default Page Size = 20)

#### Supported Filter Operations:
- Equal
- NotEqual
- Contains
- GreaterThan
- LessThan
- GreaterThanAndEqual
- LessThanAndEqual
- StartsWith
- EndsWith

#### Supported Order Operations:
- Ascending
- Descending

#### Usages:

Model:

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

MVC Flow Usage Sample:

```cs


[HttpGet]
public JsonResult GetUsers(FilteryRequest request) 
{
    var userList = new List<User>();
    userList.Add(new User{FirstName = "Türhan", LastName = "Yıldırım", Age = 22});
    userList.Add(new User{FirstName = "Çağla", LastName = "Yıldırım", Age = 18});

    var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery).ToList();

    return Json(response);
}

```

Usage Sample:

```cs
var userList = new List<User>();
userList.Add(new User{FirstName = "Türhan", LastName = "Yıldırım", Age = 22});
userList.Add(new User{FirstName = "Çağla", LastName = "Yıldırım", Age = 18});

var filteryQuery = new FilteryRequest
{
    AndFilters = new List<FilterItem>
    {
        new FilterItem {TargetFieldName = "name", Value = "ça", Operation = FilterOperation.Contains },
        new FilterItem {TargetFieldName = "last", Value = "Yıl", Operation = FilterOperation.Contains, , CaseSensitive = true}
    },
    OrderOperations = new Dictionary<string, OrderOperation>()
    {
        {"name", OrderOperation.Asc}
    },
    PageNumber = 1,
    PageSize = 2
};

var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery).ToList();

```



### Release Notes

#### 1.0.0
* Base releases
