using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class LoginVM
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }
	}
}
