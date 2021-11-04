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
    public class CustomersDB
    {

        private IConfiguration Configuration { get; }

        public CustomersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


       
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
    }
}
