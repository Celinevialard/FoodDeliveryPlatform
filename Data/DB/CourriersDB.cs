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
					string query = @"SELECT * FROM DeleveryZone 
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

		public List<Courrier> GetCourrierByLocalite(int depart, int arriver)
		{
			List<Courrier> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Courrier c
							INNER JOIN DeleveryZone dz ON c.courrierId = dz.courrierID 
							WHERE dz.locationID = @departId AND
							c.courrierId IN (
											SELECT DISTINCT dz2.courrierId FROM DeleveryZone dz2 WHERE dz2.LocationId = @arriveId)";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@departId", depart);
					cmd.Parameters.AddWithValue("@arriveId", arriver);
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
