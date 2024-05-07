using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Data
{
	public class Bank
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string contactPerson	 { get; set;}
		public string schedule { get; set;}
	}
	
}
