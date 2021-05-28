# transavia-netcore
.NET Core library for the Transavia public APIs

# Getting Started
To make your first API call, you will need to [sign up](https://tst.developer.transavia.com/) for the Transavia Api Program

```csharp
public class Startup
    {
        // ....
        public void ConfigureServices(IServiceCollection services)
        {
           // ...
           services.AddTransaviaServices(false, "YOUR_API_KEY");
        }
    }
```
