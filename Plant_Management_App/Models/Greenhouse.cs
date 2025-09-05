using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Greenhouse
    {
        [Key]
        public int GreenhouseID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        [Display(Name = "Size (sq ft)")]
        public decimal SizeSqFt { get; set; }
        [Display(Name = "Temperature Controlled")]
        public bool TemperatureControlled { get; set; }
        [Display(Name = "Humidity Controlled")]
        public bool HumidityControlled { get; set; }

        public ICollection<PlantBatch>? PlantBatches { get; set; }
        public ICollection<EnvironmentLog>? environmentLogs { get; set; }


    }
}
