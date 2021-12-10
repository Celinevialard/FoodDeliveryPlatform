﻿
namespace DTO
{
	public class Person
	{
		public int PersonId { get; set; }

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Customer CustomerInfo { get; set; }

		public Courrier CourrierInfo { get; set; }

	}
}
