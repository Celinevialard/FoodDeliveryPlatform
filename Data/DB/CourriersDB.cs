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

		public List<int> GetDeliveryZoneByCourrierId(int id)
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

	}
}
