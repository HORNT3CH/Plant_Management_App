using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [DisplayName("Order Date")]        
        public DateTime OrderDate { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; } = 0.0m;

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
