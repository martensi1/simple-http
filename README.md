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

## License

This repository is licensed with the [MIT](LICENSE) license

## Author

Simon Mårtensson (martensi)
