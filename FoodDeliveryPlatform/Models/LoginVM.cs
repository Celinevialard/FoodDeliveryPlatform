using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class LoginVM
	{
		[EmailAddress]
		[Required]
		[StringLength(50)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required]
		[StringLength(25)]
		public string Password { get; set; }
	}
}
