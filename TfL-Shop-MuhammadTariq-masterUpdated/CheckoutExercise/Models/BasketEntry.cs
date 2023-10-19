using System.ComponentModel.DataAnnotations;

namespace CheckoutExercise.Models
{
    public class BasketEntry
    {
        [Key]
        [Required(ErrorMessage = "Stock Reference Number is required.")]
        public int StockReferenceNumber { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0.")]
        public int Quantity { get; set; }
    }
}