using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        public int PlantID { get; set; }
        public Plant? Plant { get; set; }

        public int BatchID { get; set; }
        public PlantBatch? PlantBatch { get; set; }

        public int QuantityAvailable { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        public string? Location { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
