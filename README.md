# SimpleHttp

Small and simple HTTP client library

Features:
  * Send GET and POST requests

## Example
```csharp
using (var client = new SimpleHttp())
{
	var json = httpClient.Get("https://example.com/data");
	Console.WriteLine(json);
}
```

## Installation
Install with NuGet:

```
dotnet add package PilotAppLibs.SimpleHttp
```

## Documentation

Learn how to use by reading the [documentation](https://martensi1.gitlab.io/aviation-libraries/simple-http/index.html)

## Author

Simon Mårtensson (martensi)
