TinyHttp - a tiny C# HTTP server

Copyright (C) 2012 Matt Jamieson
Modifications (C) 2015 Nate Barbettini

```c#
public class Program : RequestProcessor
{
    public Program()
    {
		// Optional callback for logging, etc.
		OnRequest = (route, request) => Log(route, request);
	
        Get["/"] = (param, request) => new HtmlResponse("<h1>Welcome</h1>");
        Get["/hello/{name}"] = (param, request) => new HtmlResponse(String.Format("<h1>Hello, {0}</h1>", param.name));
    }
	
	private void Log(Route route, Request request)
	{
		Console.WriteLine("{0} {1} [{2}]", route.Method, route.Path, request.Url);
	}
	
    public static void Main()
    {
        var host = new TinyHttpHost("http://localhost:9999/", new Program());
        host.Start();

        while (true) ;
    }
}
```