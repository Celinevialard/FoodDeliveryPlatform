using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class PersonsDB : IPersonsDB
	{
		private IConfiguration Configuration { get; }

		public PersonsDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
		/// <summary>
		/// Obtention de la personne par son login, password et de ses informations spécifiques (livreur ou client)
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public Person GetPersonByLogin(string login, string password)
		{
			Person result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Person p
							LEFT JOIN Customer c ON p.personId = c.personId
							LEFT JOIN Courrier cr ON p.personId = cr.personId
							WHERE login = @login AND password = @password";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@login", login);
					cmd.Parameters.AddWithValue("@password", password);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							result = ReadPerson(dr);

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
		/// Obtention de la personne par son customer id et de ses informations spécifiques (livreur ou client)
		/// </summary>
		/// <param name="customerId"></param>
		/// <returns></returns>
		public Person GetPersonByCustomer(int customerId)
		{
			Person result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Person p
								LEFT JOIN Courrier cr ON p.personId = cr.personId
								LEFT JOIN Customer c ON p.personId = c.personId
							WHERE c.customerId = @customerId";

					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@customerId", customerId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							result = ReadPerson(dr);

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
		/// Obtention de la personne par son courrier id et de ses informations spécifiques (livreur ou client)
		/// </summary>
		/// <param name="courrierId"></param>
		/// <returns></returns>
		public Person GetPersonByCourrier(int courrierId)
		{
			Person result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Person p
								LEFT JOIN Courrier cr ON p.personId = cr.personId
								LEFT JOIN Customer c ON p.personId = c.personId
							WHERE cr.courrierId = @courrierId";

					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@courrierId", courrierId);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							result = ReadPerson(dr);

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
		/// Controle si l'identifiant de connection existe déjà
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public bool CheckLoginExist(string login)
		{
			bool result = false;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"SELECT * FROM Person p
							WHERE login = @login";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("@login", login);
					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						result = dr.Read();
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
		/// Ajout d'un personne dans la table Person
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Mise à jour d'une personne
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public bool UpdatePerson(Person person)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "UPDATE Person SET Firstname = @Firstname, Name = @Name, Login = @Login WHERE PersonId = @PersonId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("PersonId", person.PersonId);
					cmd.Parameters.AddWithValue("Firstname", person.Firstname);
					cmd.Parameters.AddWithValue("Name", person.Lastname);
					cmd.Parameters.AddWithValue("Login", person.Login);

					cn.Open();

					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{

				throw e;
			}
			return true;
		}

		/// <summary>
		/// Rempli un objet person avec les infos qui viennent de la base de donnée
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		private Person ReadPerson(SqlDataReader dr)
		{
			Person person = new Person();
			person.PersonId = (int)dr["personId"];
			person.Firstname = (string)dr["firstname"];
			person.Lastname = (string)dr["name"];
			person.Login = (string)dr["login"];
			person.Password = (string)dr["password"];


			if (dr["customerId"] != DBNull.Value)
			{
				person.CustomerInfo = new Customer();
				person.CustomerInfo.CustomerId = (int)dr["customerId"];
				if (dr["address"] != DBNull.Value)
					person.CustomerInfo.Address = (string)dr["address"];
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
