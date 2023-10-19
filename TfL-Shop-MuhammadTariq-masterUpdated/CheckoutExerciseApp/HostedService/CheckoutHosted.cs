using CheckoutExercise;
using CheckoutExercise.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutExerciseApp.HostedService
{
    public class CheckoutHosted : IHostedService, IDisposable
    {
        private IServiceScopeFactory Services { get; }

        public CheckoutHosted(IServiceScopeFactory services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = Services.CreateScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<CheckoutService>();

            
            var customer = new CustomerAccount(); 
            var order = new Order(); 

            // Call PlaceOrder with the customer and order objects
            bool orderPlaced = scopedService.PlaceOrder(customer, order);

            Console.WriteLine("Finishing CheckOutService...");

            Thread.Sleep(5000); // Delay for 5 seconds
            Environment.Exit(0);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // No need to do anything here since we are not using a timer.
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // No need to do anything here since we are not using a timer.
        }
    }
}
