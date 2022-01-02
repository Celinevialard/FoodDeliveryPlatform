
using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public enum OrderStatusEnum : int
	{
		Unknown = 0,
		[Display(Name = "En cours")]
		Delivering = 1,
		[Display(Name = "Livré")]
		Delivered = 2,
		[Display(Name = "Annulé")]
		Cancelled = 3
	}
}
