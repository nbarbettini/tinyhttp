using System;

namespace TinyHttp.Tests
{
    public class Program : RequestProcessor
    {
        public Program()
        {
            Get["/"] = (p, req) => new HtmlResponse("<h1>Welcome</h1>");
            Get["/hello/{name}"] = (p, req) => new HtmlResponse(String.Format("<h1>Hello, {0}</h1>", Uri.UnescapeDataString(p.name)));
        }

        public static void Main()
        {
            var host = new TinyHttpHost("http://localhost:9999/", new Program());
            host.Start();

            while (true) ;
        }
    }
}