using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class OrderManager
	{
		private IOrdersDB OrdersDb { get; }
		private IDishesDB DishesDb { get; }
		private IRestaurantsDB RestaurantsDb { get; }
		private ICourriersDB CourriersDb { get; }
		private ICustomersDB CustomersDb { get; }
		public OrderManager(IConfiguration conf)
		{
			OrdersDb = new OrdersDB(conf);
			CustomersDb = new CustomersDB(conf);
			DishesDb = new DishesDB(conf);
			CourriersDb = new CourriersDB(conf);
			RestaurantsDb = new RestaurantsDB(conf);
		}

		public List<Order> GetOrdersByCourrier(int courrierId)
		{
			return OrdersDb.GetOrdersByCourrier(courrierId);
		}

		/// <summary>
		/// Création et validation d'une commande
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public Order CreateOrder(Order order)
		{
			CheckAllDishFromSameRestaurant(order);
			order.TotalAmount = CalculTotalAmount(order);
			order.CourrierId = SetCourrierByOrder(order);
			return OrdersDb.InsertOrder(order);
		}

		/// <summary>
		/// Calcule du montant total de la commande
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public decimal CalculTotalAmount(Order order)
		{
			decimal totalAmout = 0;
			foreach(OrderDetail detail in order.Details)
			{
				Dish dish = DishesDb.GetDishById(detail.DishId);
				totalAmout += dish.Price * detail.Quantity;
			}
			return totalAmout;
		}

		/// <summary>
		/// Renvoie une liste de tranche horaire pour se faire livrer
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public List<DateTime> GetDateDelivery(Order order)
		{
			List<DateTime> dateDelivery = new List<DateTime>();

			DateTime firstTime = DateTime.Now;
			firstTime.AddMinutes(15 - firstTime.Minute % 15);
			dateDelivery.Add(firstTime);
			for (int i = 1; i < 10; i++)
			{
				dateDelivery.Add(dateDelivery[i - 1].AddMinutes(15));
			}

			(int depart, int arriver) = GetLocalites(order);
			List<Courrier> courriers = CourriersDb.GetCourrierByLocalite(depart, arriver);

			foreach (Courrier courrier in courriers)
			{
				courrier.Orders = OrdersDb.GetOrdersByCourrier(courrier.CourrierId);
			}

			return dateDelivery;
		}
		
		/// <summary>
		/// controle que tout le splats de la command viennent du même restaurant
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		private bool CheckAllDishFromSameRestaurant(Order order)
		{
			int restaurantId = 0;
			foreach (OrderDetail detail in order.Details)
			{
				Dish dish = DishesDb.GetDishById(detail.DishId);
				if (restaurantId == 0)
					restaurantId = dish.RestaurantId;
				else if (restaurantId != dish.RestaurantId)
					return false;
			}
			return true;
		}
		
		/// <summary>
		/// Récupération de la localité du restaurant et du customer d'une commande
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		private (int,int) GetLocalites(Order order)
		{
			Customer customer = CustomersDb.GetCustomer(order.CustomerId);
			Dish dish = DishesDb.GetDishById(order.Details[0].DishId);
			Restaurant restaurant = RestaurantsDb.GetRestaurantsById(dish.RestaurantId);

			return (restaurant.LocationId, customer.LocationId);
		}

		/// <summary>
		/// Attribué un livreur pour une commande
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		private int SetCourrierByOrder(Order order)
		{
			(int depart, int arriver) = GetLocalites(order);
			List<Courrier> courriers = CourriersDb.GetCourrierByLocalite(depart, arriver);

			foreach(Courrier courrier in courriers)
			{
				List<Order> orders = OrdersDb.GetOrdersByCourrier(courrier.CourrierId);
				int nbrOrder = 0;
				foreach(Order o in orders)
				{
					if (o.OrderDate.AddMinutes(15) <= order.OrderDate && o.OrderDate.AddMinutes(-15) >= order.OrderDate)
						nbrOrder++;
				}
				if(nbrOrder < 5)
					return courrier.CourrierId;
			}
			return 0;
			
		}
	}
}
