using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestBLL
{
	class Program
	{
		public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				 .Build();

		static void Main(string[] args)
		{
			Order order = new Order
			{
				CustomerId = 4,
				Details = new List<OrderDetail>()
				{
					new OrderDetail(){
						DishId=4,
						Quantity=1						
					},
					new OrderDetail(){
						DishId=5,
						Quantity=3
					}
				}
			};

			OrderManager orderManager = new OrderManager(Configuration);

			var dates = orderManager.GetDateDelivery(order);
			foreach (var date in dates)
			{
				Console.WriteLine(date);
			}

			order.OrderDate = dates[0];
			order.Status = OrderStatusEnum.Delivering;

			var orderNew = orderManager.CreateOrder(order);
		}
	}
}
