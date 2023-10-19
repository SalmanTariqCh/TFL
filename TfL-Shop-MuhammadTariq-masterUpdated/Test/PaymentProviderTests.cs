using System.Collections.Generic;
using CheckoutExercise;
using CheckoutExercise.Models;
using CheckoutExercise.Services;
using FluentAssertions;
using NUnit.Framework;
using Moq;
using CheckoutExercise.Provider;
using CheckoutExercise.Services.Interfaces;

namespace Tests
{
    /// <summary>
    ///     The test below passes, passes, but there are defects in the code. The current unit test is not a very good test.
    ///     You may wish to refactor it and add extra tests, or scrap it entirely and start from scratch, it’s up to you.
    ///     The following NuGet packages are installed:
    ///     - <see cref="NUnit"/>
    ///     - <see cref="FluentAssertions"/>
    ///     - <see cref="Moq"/>
    ///     You may wish to use additional or alternative testing packages, it's up to you.
    /// </summary>
    [TestFixture]
    public class CheckoutServiceTests
    {
        private Mock<NotificationProvider>  notifacationProviderMock;
        private Mock<IPriceCalculatorService> priceCalculatorMock;
        private CustomerAccount customer;
        private List<BasketEntry> basket;
        private Order order;
        private CheckoutService checkoutService;
        private int expectedBalance;
        private int orderCount;

        [SetUp]
        public void SetUp()
        {
            notifacationProviderMock = new Mock<NotificationProvider>();
            priceCalculatorMock = new Mock<IPriceCalculatorService>();
            customer = new CustomerAccount 
            { 
                Balance = 100 
            };

            basket = new List<BasketEntry> 
            { 
                new BasketEntry 
                { 
                    StockReferenceNumber = 1, 
                    UnitPrice = 50, 
                    Quantity = 2 
                } 
            };

            expectedBalance = 0;
            orderCount = 1;

            order = new Order { Basket = basket };
            checkoutService = new CheckoutService(notifacationProviderMock.Object, priceCalculatorMock.Object);

            priceCalculatorMock.Setup(p => p.CalculateBasketPrice(basket)).Returns(100); // Mocking the price calculation.

        }


        [Test]
        public void PlaceOrder_SufficientBalance_ReturnsTrue()
        {
            
            // Act
            bool result = checkoutService.PlaceOrder(customer, order);
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(expectedBalance, Is.EqualTo(customer.Balance)); 
                Assert.That(orderCount, Is.EqualTo(customer.OrdersPlaced.Count)); 
            });

        }

        [Test]
        public void PlaceOrder_InsufficientBalance_ReturnsFalse()
        {
            // Arrange
            notifacationProviderMock = new Mock<NotificationProvider>();
            priceCalculatorMock = new Mock<IPriceCalculatorService>();
            customer = new CustomerAccount 
            { 
                Balance = 10 
            };
            
            basket = new List<BasketEntry> { 
                new BasketEntry 
                { 
                    StockReferenceNumber = 1, 
                    UnitPrice = 50, 
                    Quantity = 2 
                } 
            };
            
            order = new Order 
            { 
                Basket = basket 
            };
            
            checkoutService = new CheckoutService(notifacationProviderMock.Object, priceCalculatorMock.Object);

            priceCalculatorMock.Setup(p => p.CalculateBasketPrice(basket)).Returns(100); // Mocking the price calculation.

            // Act
            bool result = checkoutService.PlaceOrder(customer, order);
            
            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PlaceOrder_ExceptionOccurs_ReturnsFalse()
        {
            // Arrange
            notifacationProviderMock = new Mock<NotificationProvider>();
            priceCalculatorMock = new Mock<IPriceCalculatorService>();
            customer = new CustomerAccount 
            { 
                Balance = 100 
            };
            
            basket = new List<BasketEntry> 
            { 
                new BasketEntry 
                { 
                    StockReferenceNumber = 1, 
                    UnitPrice = 50, 
                    Quantity = 2 
                } 
            };
            
            order = new Order 
            { 
                Basket = basket 
            };

            checkoutService = new CheckoutService(notifacationProviderMock.Object, priceCalculatorMock.Object);

            priceCalculatorMock.Setup(p => p.CalculateBasketPrice(basket)).Throws(new Exception("Test exception")); // Mocking an exception.

            // Act
            bool result = checkoutService.PlaceOrder(customer, order);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}