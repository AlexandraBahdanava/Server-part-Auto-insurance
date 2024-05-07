using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
    public class Services
    {
        [Key]
        public int Id { get; set; }
        public string text { get; set; }
        public string additionalCost { get; set; }

        [ForeignKey("InsurancePackagesId")]
        public int InsurancePackagesId { get; set; }
        public InsurancePackages InsurancePackages { get; set; }
    }
}
