using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
	public class Statements
	{
		[Key]
		public int Id { get; set; }
		public string Text { get; set; }
		[ForeignKey("UserId")]
		public int UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("InsurancePackagesId")]
		public int InsurancePackagesId { get; set; }
		public InsurancePackages InsurancePackages { get; set; }
	}
}
