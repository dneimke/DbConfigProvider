# DbConfigProvider

This sample application demonstrates how to add a custom ConfigurationProvider to an
ASP.NET MVC Core 2.1 web application.

The intent of doing this is so that application configuration settings can be managed
and versioned from the database.  

Using a technique such as this allows administrative users to manage settings from a web 
page.

The custom provider is added in the Program.cs file

```C#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(AddConfiguration)
        .UseStartup<Startup>();


private static void AddConfiguration(WebHostBuilderContext context, 
    IConfigurationBuilder builder)
{
    var configuration = builder.Build();
    var connectionString = configuration.GetConnectionString("Database");
    builder.AddDemoDbProvider(options => options.UseSqlServer(connectionString));
}
```

Our provider reads from an ApplicationConfigurationItem table and adds items
to the Data configuration dictionary.

```C#
var items = await context.ApplicationConfigurationItem
    .AsNoTracking()
    .ToListAsync();

foreach (var item in items)
{
    Data.Add(item.Title, item.Value);
}
```
You can see an example of consuming the configuration data in HomeController.cs

```C#
public IActionResult Index()
{
    var foo = _configuration["Foo"]; // provided from FileProvider
    var dbValue = _configuration["One"]; // provided by custom DbProvider

    ViewData["Foo"] = foo;
    ViewData["DbValue"] = dbValue;

    return View();
}
```
