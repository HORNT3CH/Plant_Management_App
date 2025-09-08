using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }
        public Order? Order { get; set; }

        public int InventoryID { get; set; }
        public Inventory? Inventory { get; set; }

        public int Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;        

    }
}
