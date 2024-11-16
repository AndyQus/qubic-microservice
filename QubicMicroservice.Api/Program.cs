using DotNetEnv;

namespace QubicMicroservice.Api.WebAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        Env.Load("../.env");
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
