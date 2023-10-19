using CheckoutExercise.Models;
using System.Collections.Generic;

namespace CheckoutExercise.Services.Interfaces
{
    public interface IPriceCalculatorService
    {
        int CalculateBasketPrice(List<BasketEntry> basketItems);
    }
}