using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class PersonsDB : IPersonsDB
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

		//TODO addperson 
		public Person AddPerson(Person person)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "INSERT INTO Person(Firstname, Name, Login, Password) VALUES (@Firstname, @Name, @Login, @Password); SELECT SCOPE_IDENTITY()";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("Firstname", person.Firstname);
					cmd.Parameters.AddWithValue("Name", person.Lastname);
					cmd.Parameters.AddWithValue("Login", person.Login);
					cmd.Parameters.AddWithValue("Password", person.Password);

					cn.Open();

					person.PersonId = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			catch (Exception e)
			{

				throw e;
			}
			return person;
		}

		private Person ReadPerson(SqlDataReader dr)
		{
			Person person = new Person();

			person.Firstname = (string)dr["firstname"];
			person.Lastname = (string)dr["name"];
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
