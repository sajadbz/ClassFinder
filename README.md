# Class Finder
Use `Bz.Helper.GetClassAndMethods<T>()` method and custom your class or methods with `[BzDescriptionAttribute]` to get list of all class with own methods that you select with this attribute.
[My web site](https://bagherzadeh.info)

Create static read-only object for get list of class and method that has `[BzDescription("{your custom title}")]` Attribute and use this object in your project 
>I use this to get all action in my controller for list of permission that want to check in user role permission.
>
Find it on :
https://www.nuget.org/packages/Bz.ClassFinder/
## Example :
### Create an instance with class finder helper :
```csharp
public static readonly IReadOnlyList<BzClassInfo> Permissions = Bz.ClassFinder.Helper.GetClassAndMethods(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "","App.WebUI.dll")).ToList();
```

### How to use attribute:

```csharp
using Bz.ClassFinder.Attributes;

namespace MyProjectNameSpace
{
    [BzDescription("Locations")]
    public class LocationsController : BaseAdminController
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService stateService)
        {
            _locationService = stateService;
        }

        [BzDescription("Location List")]
        public async Task<IActionResult> Index()
        {           
            return View(_locationService.GetAll(id));
        }

        [BzDescription("Create Location")]
        public async Task<IActionResult> Create(int? id)
        {
            return View();
        }       
    }
}
```
