using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plant_Management_App.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        public string? Name { get; set; }
        [DisplayName("Contact Person")]
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public ICollection<Supply>? Supplies { get; set; }
    }
}
