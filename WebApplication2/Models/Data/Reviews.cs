using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
	public class Reviews
	{
		[Key]
		public int Id { get; set; }
		public string text{ get; set; }
		public string grade { get; set; }
		[ForeignKey("UserId")]
		public int UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("BankId")]
		public int BankId { get; set; }
		public Bank Bank { get; set; }
	}
}
