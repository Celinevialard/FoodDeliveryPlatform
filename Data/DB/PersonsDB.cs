using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class PersonsDB
	{
		//Constructeur
		private IConfiguration Configuration { get; }

		public PersonsDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// Get by login and password
		// est-ce qu'on récupère aussi les infos de courrier et de customer
		// on pourra savoir si les info courrier sont null il ne peux pas livrer 
		// si info customer null il pourra en ajouter pour commander?

		// Si trop compliquer modifier db pour une entiter customer et un entiter courrier séparer

		public List<Person> GetPersonByLogin(string login, string password)
		{
			List<Person> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Person p
							INNER JOIN Customer c ON p.personId = c.personId
							INNER JOIN Courrier cr ON p.personId = cr.personId
							WHERE login = @login AND password = @password";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@login", login);
					cmd.Parameters.AddWithValue("@password", password);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Person>();

							results.Add(ReadPerson(dr));

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
		

		public List<int> GetDeliveryZonByCourrierId(int id)
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


		// insert??

		// update??


		private Person ReadPerson(SqlDataReader dr)
		{
			Person person = new Person();

			person.FirstName = (string)dr["firstname"];
			person.Name = (string)dr["name"];
			person.Login = (string)dr["login"];
			person.Password = (string)dr["password"];


			if (dr["customerId"] != DBNull.Value)
			{
				person.CustomerInfo = new Customer();
				person.CustomerInfo.CustomerId = (int)dr["customerId"];
				person.CustomerInfo.LocationId = (int)dr["locationId"];
			}

			if (dr["courrierId"] != DBNull.Value)
			{
				person.CourrierInfo = new Courrier();
				person.CourrierInfo.CourrierId = (int)dr["courrierId"];
			}
				
			return person;
		}
	}
}
