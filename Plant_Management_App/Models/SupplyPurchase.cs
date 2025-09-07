using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class SupplyPurchase
    {
        [Key]
        public int PurchaseID { get; set; }

        public int SupplyID { get; set; }
        public Supply? Supply { get; set; }

        [DisplayName("Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Total Cost")]
        public decimal TotalCost { get; set; }
    }
}
