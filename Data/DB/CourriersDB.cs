using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class CourriersDB : ICourriersDB
	{
		private IConfiguration Configuration { get; }
		public CourriersDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		/// <summary>
		/// Inserzion d'un livreur dans la table Courrier
		/// </summary>
		/// <param name="courrier"></param>
		/// <returns></returns>
		public Courrier AddCourrier(Courrier courrier)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "INSERT INTO Courrier (PersonId) VALUES (@PersonId); SELECT SCOPE_IDENTITY()";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("PersonId", courrier.PersonId);

					cn.Open();

					courrier.CourrierId = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			catch (Exception e)
			{

				throw e;
			}
			return courrier;
		}

		/// <summary>
		/// Récupération de la liste des locationsId où livrent un livreur
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<int> GetLocationsByCourrierId(int id)
		{

			List<int> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM DeliveryZone 
							WHERE courrierId = @id";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@id", id);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<int>();

							results.Add((int)dr["locationId"]);

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


		public List<int> GetCourriersIdByLocationId(int locationId)
        {
			List<int> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
				using(SqlConnection cn = new SqlConnection(connectionString))
                {
					string query = @"SELECT * FROM Courrier c
									INNER JOIN DeliveryZone dz ON c.courrierId = dz.courrierId
									WHERE dz.locationId = @locationId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@locationId", locationId);
					cn.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<int>();


							if (dr["CourrierId"] != DBNull.Value)
								results.Add((int)dr["CourrierId"]);
								

							
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
		/// Récupétation de la liste des livreurs pour les localités de livraisons (departId: restaurant et arriveeId : chez le client)
		/// </summary>
		/// <param name="depart"></param>
		/// <param name="arrivee"></param>
		/// <returns></returns>
		public List<Courrier> GetCourrierByLocalite(int depart, int arrivee)
		{
			List<Courrier> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Courrier c
							INNER JOIN DeliveryZone dz ON c.courrierId = dz.courrierID 
							WHERE dz.locationID = @departId AND
							c.courrierId IN (
											SELECT DISTINCT dz2.courrierId FROM DeliveryZone dz2 WHERE dz2.LocationId = @arriveeId)";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@departId", depart);
					cmd.Parameters.AddWithValue("@arriveeId", arrivee);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						

						while (dr.Read())
						{
							if (results == null)
								results = new List<Courrier>();

							results.Add(ReadCourrier(dr));

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

		private Courrier ReadCourrier(SqlDataReader dr)
		{
			Courrier courrier = new Courrier();

			courrier.CourrierId = (int)dr["courrierId"];
			courrier.PersonId = (int)dr["personId"];

			return courrier;
		}
	}
}
