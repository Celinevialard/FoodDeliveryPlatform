using DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class UserEditVM
	{
		public int PersonId { get; set; }
		public string Address { get; set; }

		public int LocationId { get; set; }

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }

		public List<Location> Locations { get; set; }
	}
}
