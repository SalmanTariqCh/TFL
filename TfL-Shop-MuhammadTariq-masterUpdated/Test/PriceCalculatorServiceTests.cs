using CheckoutExercise.Models;
using CheckoutExercise.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class PriceCalculatorServiceTests
    {
        private PriceCalculatorService priceCalculator;
        private List<BasketEntry> basketItems;
        private int totalCost;
        private int expectedTotalCost;

        [SetUp]
        public void SetUp()
        {
            priceCalculator = new PriceCalculatorService();
            basketItems = new List<BasketEntry>
            {
                new BasketEntry { StockReferenceNumber = 1, UnitPrice = 30, Quantity = 3 },
                new BasketEntry { StockReferenceNumber = 2, UnitPrice = 20, Quantity = 2 }
            };
        }
        
        [Test]
        public void CalculateBasketPrice_ValidInput_ReturnsTotalCost()
        {
            // Act
            totalCost = priceCalculator.CalculateBasketPrice(basketItems);

            expectedTotalCost = 130;

            // Assert
            Assert.That(expectedTotalCost, Is.EqualTo(totalCost)); 
        }

        [Test]
        public void CalculateBasketPrice_EmptyBasket_ReturnsZero()
        {
            // Arrange
            priceCalculator = new PriceCalculatorService();
            basketItems = new List<BasketEntry>();

            // Act
            int totalCost = priceCalculator.CalculateBasketPrice(basketItems);
            expectedTotalCost = 0;

            // Assert
            Assert.That(expectedTotalCost, Is.EqualTo(totalCost));
        }

        [Test]
        public void CalculateBasketPrice_NullInput_ReturnsZero()
        {
            // Arrange
            priceCalculator = new PriceCalculatorService();

            // Act
            totalCost = priceCalculator.CalculateBasketPrice(null);
            expectedTotalCost = -1;

            // Assert
            Assert.That(expectedTotalCost, Is.EqualTo(totalCost));
        }

        [Test]
        public void CalculateBasketPrice_ExceptionOccurs_ReturnsErrorValue()
        {
            // Arrange
            priceCalculator = new PriceCalculatorService();
            basketItems = new List<BasketEntry>
            {
                new BasketEntry { StockReferenceNumber = 1, UnitPrice = 10, Quantity = 3 },
                new BasketEntry { StockReferenceNumber = 2, UnitPrice = 20, Quantity = 2 }
            };
            
            // Act
            int totalCost = priceCalculator.CalculateBasketPrice(basketItems);
            expectedTotalCost = 70;

            // Assert
            Assert.That(expectedTotalCost, Is.EqualTo(totalCost));
        }
    }
}
