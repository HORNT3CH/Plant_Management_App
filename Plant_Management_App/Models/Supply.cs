using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Supply
    {
        [Key]
        public int SupplyID { get; set; }

        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal UnitCost { get; set; }

        public ICollection<SupplyPurchase>? SupplyPurchases { get; set; }
    }
}
