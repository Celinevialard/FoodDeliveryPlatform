using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FoodDeliveryPlatform.Models
{
	public class ErrorVM
	{
		public HttpStatusCode ErrorCode { get; set; }

		public string ErrorMessage { get; set; }
	}
}
