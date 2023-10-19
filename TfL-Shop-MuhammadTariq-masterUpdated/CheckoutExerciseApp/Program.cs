using CheckoutExerciseApp.HostedService;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace CheckoutExerciseApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CheckOutExercise...");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register your hosted service
                    services.AddHostedService<CheckoutHosted>();
                });
    }
}

