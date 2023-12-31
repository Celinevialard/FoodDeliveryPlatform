﻿using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
	public class LocationsDB : ILocationsDB
	{
		private IConfiguration Configuration { get; }

		public LocationsDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Obtenir la liste de toutes les localités
		/// </summary>
		/// <returns></returns>
		public List<Location> GetLocations()
		{
			List<Location> results = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "SELECT * FROM Location ORDER BY Npa";
					SqlCommand cmd = new SqlCommand(query, cn);

					cn.Open();

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Location>();

							results.Add(ReadLocality(dr));
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
		/// Obtenir la localité par son Id
		/// </summary>
		/// <param name="LocationId"></param>
		/// <returns></returns>
		public Location GetLocationById(int LocationId)
		{
			Location result = null;
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "SELECT * FROM Location WHERE LocationId = @LocationId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("LocationId", LocationId);

					cn.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{ 
							result = ReadLocality(dr);
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
		/// Rempli un objet location avec les infos qui viennent de la base de donnée
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		private Location ReadLocality(SqlDataReader dr)
		{
			Location location = new Location();

			if (dr["LocationId"] != DBNull.Value)
				location.LocationId = (int)dr["LocationId"];

			if (dr["NPA"] != DBNull.Value)
				location.NPA = (string)dr["NPA"];

			if (dr["Location"] != DBNull.Value)
				location.Locality = (string)dr["Location"];


			return location;
		}
	}
}
