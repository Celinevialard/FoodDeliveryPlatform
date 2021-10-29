using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class RestaurantsDB
	{
		//Constructeur
		private IConfiguration Configuration { get; }

		public RestaurantsDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// List by localité
		public List<Restaurant> GetRestaurantsByLocalite(int localiteId)
		{
			List<Restaurant> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "Select * from Restaurant WHERE locationId = @localiteId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@localiteId", localiteId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Restaurant>();

							results.Add(ReadRestaurant(dr));

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

		private Restaurant ReadRestaurant(SqlDataReader dr)
		{
			Restaurant restaurant = new Restaurant();

			restaurant.RestaurantId = (int)dr["restaurantId"];
			restaurant.Name = (string)dr["restaurantName"];


			if (dr["locationId"] != DBNull.Value)
				restaurant.LocationId = (int)dr["locationId"];

			if (dr["restaurantDescription"] != DBNull.Value)
				restaurant.Description = (string)dr["restaurantDescription"];

			return restaurant;
		}
	}
}
