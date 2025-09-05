using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class PlantBatch
    {
        [Key]
        public int BatchID { get; set; }
        
        public int PlantID { get; set; }
        public Plant? Plant { get; set; }

        public int GreenhouseID { get; set; }
        public Greenhouse? Greenhouse { get; set; }

        [Display(Name = "Seed Date")]
        public DateTime SeedDate { get; set; }
        [Display(Name = "Transplant Date")]
        public DateTime? TransplantDate { get; set; }
        [Display(Name = "Quantity Planted")]
        public int QuantityPlanted { get; set; }
        [Display(Name = "Expected Harvest Date")]
        public DateTime? ExpectedHarvestDate { get; set; }
        public string? Notes { get; set; }

        public ICollection<Inventory>? Inventories { get; set; }
    }
}
