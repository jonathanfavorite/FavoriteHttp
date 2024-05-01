namespace FavoriteHttp.Playground
{
    using FavoriteHttp.CORE.interfaces;
    using FavoriteHttp.CORE.models;
    using FavoriteHttp.interfaces;
    using CORE.enums;
    
    internal class ExampleJsonResponse
    {
        public string Origin { get; set; }
        public string Url { get; set; }
    }
    internal class ExamplePostResponse
    {
        public Dictionary<string, string> Form {get; set;}
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new FavoriteHttpConfig<ExampleJsonResponse>()
                 .WithPayloadType(PayloadType.JSON)
                 .WithMethodType(MethodType.GET)
                    .WithHeader("test", "jonathan")
                    .WithHeader("authorization", "bearer");

            await new FavoriteHttpClient<ExampleJsonResponse>("https://httpbin.org/get")
                  .WithSettings(config)
                  .WithSuccess((response, model) =>
                  {
                      Console.WriteLine($"Success with status code: {response.StatusCode}");
                      Console.WriteLine($"Origin: {model.Origin}");
                      Console.WriteLine($"URL: {model.Url}");
                  })
                  .WithFailure(errorDetails =>
                  {
                      Console.WriteLine($"Failed: {errorDetails}");
                  })
                  .SendRequestAsync();


            Console.WriteLine("\r\n next \r\n");


            await new FavoriteHttpClient<string>("https://httpbin.org/get")
                .WithSuccess((response, model) =>
                {
                    Console.WriteLine(model);
                })
                .WithFailure((err) =>
                {
                    Console.WriteLine(err);
                })
                .SendRequestAsync();

            Console.WriteLine("\r\n next \r\n");

            var testConfig1 = new FavoriteHttpConfig<ExamplePostResponse>()
                .WithMethodType(MethodType.POST);

            await new FavoriteHttpClient<ExamplePostResponse>("https://httpbin.org/post")
                .WithSettings(testConfig1)
                .WithBody(new { test = "jonathan", test2 = "test2" })
                .WithSuccess((response, model) =>
                {
                    Console.WriteLine($"Success with status code: {response.StatusCode}");
                    Console.WriteLine($"Form: {model.Form}");
                })
                .WithFailure(errorDetails =>
                {
                    Console.WriteLine($"Failed: {errorDetails}");
                })
                .SendRequestAsync();

            Console.WriteLine("\r\n next \r\n");

            await new FavoriteHttpClient<string>("https://marcomovies.com")
                .WithSuccess((response, html) =>
                {
                    Console.WriteLine(html);
                })
                .WithFailure((err) =>
                {
                    Console.WriteLine(err);
                })
                .SendRequestAsync();
            Console.ReadLine();
        }
    }
}