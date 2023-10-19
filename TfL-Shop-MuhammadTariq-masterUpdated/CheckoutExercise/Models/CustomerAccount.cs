using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckoutExercise.Models
{
    public class CustomerAccount
    {
        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Balance must be greater than or equal to 0.")]
        public int Balance { get; set; }

        public List<Order> OrdersPlaced { get; set; } = new List<Order>();
    }
}