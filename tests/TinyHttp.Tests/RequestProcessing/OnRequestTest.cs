namespace TinyHttp.Tests.RequestProcessing
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class OnRequestTest
    {
        public class ShimProcessor : RequestProcessor
        {
            public ShimProcessor(string path, Response response, Action<Route, Request> onRequest)
            {
                OnRequest = onRequest;
                Get[path] = (p, r) => response;
            }
        }

        [Test]
        public void OnRequestCallback()
        {
            var invoked = false;

            var fakeResponse = new TextResponse("OK");
            var fakeRequest = new Request("GET", new Uri("http://foobar/baz"), 0, null, null);

            var processor = new ShimProcessor("/baz", fakeResponse, (route, request) =>
            {
                // Inspect callback data
                route.Method.Should().Be("GET");
                route.Path.Should().Be("/baz");
                request.Should().Be(fakeRequest);

                invoked = true;
            });

            processor.HandleRequest(fakeRequest)
                .Should().Be(fakeResponse);
            
            invoked.Should().BeTrue();
        }
    }
}