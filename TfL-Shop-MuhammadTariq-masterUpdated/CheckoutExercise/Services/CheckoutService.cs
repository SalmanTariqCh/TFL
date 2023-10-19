using System;
using System.Linq;
using CheckoutExercise.Models;
using CheckoutExercise.Provider;
using CheckoutExercise.Services;
using CheckoutExercise.Services.Interfaces;

namespace CheckoutExercise
{
    /// <summary>
    ///     The code represents is a simple checkout process through which customer can pay for items from a pre-paid balance.
    ///     This is one of the classes which is being tested as part of the excercise. The other is <see cref="PriceCalculator"/>
    /// </summary>
    public class CheckoutService : ICheckoutService
    {
        private readonly NotificationProvider _emailProvider;
        private readonly IPriceCalculatorService _priceCalculator;

        public CheckoutService(NotificationProvider emailProvider, IPriceCalculatorService priceCalculator)
        {
            _emailProvider = emailProvider;
            _priceCalculator = priceCalculator;
        }

        public bool PlaceOrder(CustomerAccount customer, Order order)
        {
            try
            {
                var totalCost = _priceCalculator.CalculateBasketPrice(order.Basket);

                if (totalCost > customer.Balance)
                {
                    // Insufficient balance, raise an exception or return an error code.
                    throw new InvalidOperationException("Insufficient balance to place the order.");
                }

                _emailProvider.SendOrderNotification(totalCost, order.Basket.Sum(bi => bi.Quantity));

                customer.Balance -= totalCost;
                customer.OrdersPlaced.Add(order);

                return true;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Order placement failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }
    }
}