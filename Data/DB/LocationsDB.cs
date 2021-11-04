using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class LocationsDB
    {
        private IConfiguration Configuration { get; }

        public LocationsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Location> GetLocations()
        {
            List<Location> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Location";
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

        public List<Location> GetLocationByNPA(string NPA)
        {
            List<Location> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using(SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Location WHERE NPA = @NPA";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("NPA", NPA);

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

        //Ne prend que une loacalité
        public Location GetLocationByLocality(string Locality)
        {
            Location result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Location WHERE Location = @Locality";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("Locality", Locality);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
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
                        while (dr.Read())
                        {
                            if (result == null)
                                result = new Location();
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
