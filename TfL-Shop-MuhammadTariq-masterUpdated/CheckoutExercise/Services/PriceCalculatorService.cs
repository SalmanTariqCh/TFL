using System;
using System.Collections.Generic;
using CheckoutExercise.Models;
using CheckoutExercise.Services.Interfaces;

namespace CheckoutExercise.Services
{
    /// <summary>
    ///     The code provides a service to calculat a total cost given a list of <see cref="BasketEntry"/>.
    ///     This is one of the classes which is being tested as part of the excercise. The other is <see cref="CheckoutService"/>
    /// </summary>
    public class PriceCalculatorService : IPriceCalculatorService
    {
        public int CalculateBasketPrice(List<BasketEntry> basketItems)
        {
            try
            {
                int totalCost = 0;
                foreach (var item in basketItems)
                {
                    totalCost += ((int)item.UnitPrice * item.Quantity);         //Improved the logic
                }
                return totalCost;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during price calculation: {ex.Message}");
                return -1; // Or another suitable error value
            }
        }
    }
}