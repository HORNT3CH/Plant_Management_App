using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class EnvironmentLog
    {
        [Key]
        public int LogID { get; set; }

        public int GreenhouseID { get; set; }
        public Greenhouse? Greenhouse { get; set; }

        [DisplayName("Log Date")]
        public DateTime LogDate { get; set; }
        [DisplayName("Temperature (°C)")]
        public decimal TermperatureC { get; set; }
        [DisplayName("Humidity (%)")]
        public decimal HumidityPercent { get; set; }
        public string? Notes { get; set; }
    }
}
