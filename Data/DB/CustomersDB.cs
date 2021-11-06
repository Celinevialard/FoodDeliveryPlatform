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
	public class CustomersDB : ICustomersDB
	{

		private IConfiguration Configuration { get; }

		public CustomersDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Insertion d'un client dans la table Customer
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public Customer AddCustomer(Customer customer)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "INSERT INTO Customer (LocationId, PersonId) VALUES (@LocationId, @PersonId); SELECT SCOPE_IDENTITY()";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("LocationId", customer.LocationId);
					cmd.Parameters.AddWithValue("PersonId", customer.PersonId);

					cn.Open();

					customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			catch (Exception e)
			{

				throw e;
			}
			return customer;
		}

		/// <summary>
		/// Récupération d'un customer dans la table customer par son Id
		/// </summary>
		/// <param name="customerId"></param>
		/// <returns></returns>
		public Customer GetCustomer(int customerId)
		{
			Customer result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Customer
							WHERE customerID = @customerID";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@customerID", customerId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							result = ReadCustomer(dr);

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

		private Customer ReadCustomer(SqlDataReader dr)
		{
			Customer entity = new Customer();

			entity.CustomerId = (int)dr["customerId"];
			entity.PersonId = (int)dr["personId"];
			entity.LocationId = (int)dr["locationId"];

			return entity;
		}
	}
}
