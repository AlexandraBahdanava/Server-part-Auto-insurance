using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models.Data
{
	public class AuthoriationData
	{
		[Key]
		public int Id { get; set; }
		public string Login {  get; set; }
		public string Password { get; set; }
		[ForeignKey("UserId")]
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
