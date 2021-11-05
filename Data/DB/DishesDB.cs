using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class DishesDB : IDishesDB
	{
		//Constructeur
		private IConfiguration Configuration { get; }

		public DishesDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// pouvoir liste
		public List<Dish> GetDishesByRestaurantId(int restaurantId)
		{
			List<Dish> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "Select * from Dish WHERE restaurantId = @restaurantId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Dish>();

							results.Add(ReadDish(dr));

						}
					}
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return results;

		}

		private Dish ReadDish(SqlDataReader dr)
		{
			Dish dish = new Dish();

			dish.DishId = (int)dr["dishId"];
			dish.Name = (string)dr["dishName"];
			dish.Description = (string)dr["description"];
			dish.Allergies = (string)dr["allergies"];
			dish.Price = (decimal)dr["price"];
			dish.ImageLink = (string)dr["image"];

			dish.RestaurantId = (int)dr["restaurantId"];

			return dish;
		}

	}
}
