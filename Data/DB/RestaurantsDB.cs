using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class RestaurantsDB : IRestaurantsDB
	{
		
		private IConfiguration Configuration { get; }

		public RestaurantsDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Obtention de la liste des restaurant par l'id de la localité dans la table Restaurant
		/// </summary>
		/// <param name="localiteId"></param>
		/// <returns></returns>
		public List<Restaurant> GetRestaurantsByLocation(int localiteId)
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

		/// <summary>
		/// Obtention d'un restaurant par son Id dans la table Restaurant
		/// </summary>
		/// <param name="restaurantId"></param>
		/// <returns></returns>
		public Restaurant GetRestaurantsById(int restaurantId)
		{
			Restaurant result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "Select * from Restaurant WHERE restaurantId = @restaurantId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							result = ReadRestaurant(dr);
						}
					}
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return result;

		}

		/// <summary>
		/// Rempli un objet restaurant avec les infos qui viennent de la base de donnée
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
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
