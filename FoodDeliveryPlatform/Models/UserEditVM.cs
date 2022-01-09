using DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class UserEditVM
	{
		public int PersonId { get; set; }

		[StringLength(250)]
		[Required]
		public string Address { get; set; }

		[Required]
        [Range(minimum: 1, maximum: int.MaxValue,ErrorMessage = "Ce champ est obligatoire et doit être séléctionné dans la liste proposée.")]
		public int LocationId { get; set; }

		[StringLength(25)]
		[Required]
		public string Firstname { get; set; }

		[StringLength(25)]
		[Required]
		public string Lastname { get; set; }

		[EmailAddress]
		[Required]
		[StringLength(50)]
		public string Email { get; set; }

		[Required]
		[StringLength(25)]
		public string Password { get; set; }

		public List<Location> Locations { get; set; }
	}
}
