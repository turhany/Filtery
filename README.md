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

#### Type and Supported Filter Operations:
* String
    * Equal
    * NotEqual
    * Contains
    * GreaterThan > Not supported (throw Exception)
    * LessThan > Not supported (Exception)
    * GreaterThanAndEqual  > Not supported (throw Exception)
    * LessThanAndEqual > Not supported (throw Exception)
    * StartsWith
    * EndsWith 
* Integer
    * Equal
    * NotEqual
    * Contains > Not supported (Convert to Equal)
    * GreaterThan 
    * LessThan
    * GreaterThanAndEqual
    * LessThanAndEqual > Not supported (throw Exception)
    * StartsWith > Not supported (Convert to Equal)
    * EndsWith  > Not supported (Convert to Equal)
* DateTime
    * Equal
    * NotEqual
    * Contains
    * GreaterThan > Not supported (throw Exception)
    * LessThan > Not supported (throw Exception)
    * GreaterThanAndEqual  > Not supported (throw Exception)
    * LessThanAndEqual > Not supported (throw Exception)
    * StartsWith
    * EndsWith 
* Boolean
    * Equal
    * NotEqual
    * Contains > Not supported (Convert to Equal)
    * GreaterThan > Not supported (throw Exception)
    * LessThan > Not supported (throw Exception)
    * GreaterThanAndEqual > Not supported (throw Exception)
    * LessThanAndEqual > Not supported (throw Exception)
    * StartsWith > Not supported (Convert to Equal)
    * EndsWith  > Not supported (Convert to Equal)

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
    public Address Address { get; set; }
}

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
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
        mapper.Name("country").Property(p => p.Address.Country);
    }
}
```

MVC Flow Usage Sample:

```cs


[HttpGet]
public JsonResult GetUsers(FilteryRequest request) 
{
    var userList = new List<User>();
    userList.Add(new User
    {
        FirstName = "Türhan", 
        LastName = "Yıldırım", 
        Age = 22, 
        Address = new Address
        {
            Country = "Bulgaristan", 
            City = "Şumen"
        }
    });
    userList.Add(new User
    {
        FirstName = "Çağla", 
        LastName = "Yıldırım", 
        Age = 18, 
        Address = new Address
        {
            Country = "Türkiye", 
            City = "İstanbul"
        }
    });;

    var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery).ToList();

    return Json(response);
}

```

Usage Sample:

```cs
var userList = new List<User>();
userList.Add(new User
{
    FirstName = "Türhan", 
    LastName = "Yıldırım", 
    Age = 22, 
    Address = new Address
    {
        Country = "Bulgaristan", 
        City = "Şumen"
    }
});
userList.Add(new User
{
    FirstName = "Çağla", 
    LastName = "Yıldırım", 
    Age = 18, 
    Address = new Address
    {
        Country = "Türkiye", 
        City = "İstanbul"
    }
});;

var filteryQuery = new FilteryRequest
{
    AndFilters = new List<FilterItem>
    {
        new FilterItem {TargetFieldName = "country", Value = "kiye", Operation = FilterOperation.Contains}
    },
    OrFilters = new List<FilterItem>
    {
        new FilterItem {TargetFieldName = "name", Value = "ça", Operation = FilterOperation.Contains },
        new FilterItem {TargetFieldName = "last", Value = "Yıl", Operation = FilterOperation.Contains, CaseSensitive = true}
    },
    OrderOperations = new Dictionary<string, OrderOperation>()
    {
        {"name", OrderOperation.Ascending}
    },
    PageNumber = 1,
    PageSize = 2
};

var response = userList.BuildFiltery(new UserFilteryMappings(), filteryQuery).ToList();

```



### Release Notes

#### 1.0.0
* Base releases
