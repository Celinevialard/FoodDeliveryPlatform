using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class ErrorVM
	{
		public int ErrorCode { get; set; }

		public string ErrorMessage { get; set; }
	}
}
