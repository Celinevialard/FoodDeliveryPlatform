using DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FoodDeliveryPlatform.Models
{
	public class UserVM
	{
		public int PersonId { get; set; }

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		public Customer CustomerInfo { get; set; }

		public Courrier CourrierInfo { get; set; }

		public string ToString() => JsonSerializer.Serialize(this);

	}
}
