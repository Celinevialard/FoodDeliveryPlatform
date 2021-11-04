using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class LocationDB
    {
        private IConfiguration Configuration { get; }

        public LocationDB(IConfiguration configuration)
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
                    string query = "SELECT * FROM LOCATION";
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
