using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
	public class Cars
	{
		[Key]
		public int Id { get; set; }
		public string model { get; set; }
		public string number { get; set; }
		public string engineCapacity{get;set;}
		[ForeignKey ("UserId")]
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
