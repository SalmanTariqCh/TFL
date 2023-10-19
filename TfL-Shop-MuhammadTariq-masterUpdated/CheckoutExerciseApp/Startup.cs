using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using CheckoutExercise.Services;
using CheckoutExercise.Services.Interfaces;
using CheckoutExercise;

namespace CheckoutExerciseApp
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            // Add services here...
            services.AddSingleton<IPriceCalculatorService, PriceCalculatorService>();
            services.AddSingleton<CheckoutService>();

        }

        public static void Configure(IApplicationBuilder app)
        {
            //Configure middleware, if needed
        }
    }
}
