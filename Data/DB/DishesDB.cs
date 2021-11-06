using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class DishesDB : IDishesDB
    {
      
        private IConfiguration Configuration { get; }

        public DishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Obtention d'un plat par son Id dans la table Dish
        /// </summary>
        /// <param name="Dishid"></param>
        /// <returns></returns>
        public Dish GetDishById(int Dishid)
        {
            Dish result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish WHERE IdDish = @DishId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("DishId", Dishid);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = ReadDish(dr);
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
        /// Récupération de la liste de tout les plats d'un restaurant par son Id dans la table Dish
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public List<Dish> GetDishesByRestaurantId(int restaurantId)
        {
            List<Dish> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish WHERE restaurantId = @restaurantId";
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

            dish.DishId = (int)dr["IdDish"];
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
