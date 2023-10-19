using CheckoutExercise.Models;

namespace CheckoutExercise.Services.Interfaces
{
    public interface ICheckoutService
    {
        bool PlaceOrder(CustomerAccount customer, Order order);
    }
}