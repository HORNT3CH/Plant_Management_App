using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Plant
    {
        [Key]
        public int PlantID { get; set; }
        [DisplayName("Plant Name")]
        public string? CommonName { get; set; }
        [DisplayName("Scientific Name")]
        public string? ScientificName { get; set; }
        [DisplayName("Plant Type")]
        public string? PlantType { get; set; }
        public string? Description { get; set; }
        [DisplayName("Growing Season")]
        public string? GrowingSeason { get; set; }
        [DisplayName("Sun Requirements")]
        public string? SunRequirements { get; set; }
        [DisplayName("Water Requirements")]
        public string? WaterRequirements { get; set; }
        [DisplayName("Soil Type")]
        public string? SoilType { get; set; }
        [DisplayName("Plant Image")]
        public string? ImagePath { get; set; } // e.g., "images/plants/plant1.jpg"

        public ICollection<PlantBatch>? PlantBatches { get; set;}
        public ICollection<Inventory>? Inventories { get; set; }
    }
}
