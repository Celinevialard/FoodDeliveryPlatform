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
	public class OrdersDB
	{
		// List par courrier (que de snon livré)

		// Liste par customer??

		// Insert -> attention insert aussi dans details!

		//TODO Update status.param enum + idorder
		private IConfiguration Configuration { get; }

		public OrdersDB(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public Order UpdateOrder(Order order, OrderStatusEnum status)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "UPDATE Orders SET Status = @Status WHERE OrderId = @OrderId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("Status", status);
					cmd.Parameters.AddWithValue("Orderid", order.OrderId);

					cn.Open();
					cmd.ExecuteNonQuery();
					order.Status = status;
				}
			}
            catch (Exception e)
            {
                throw e;
            }
			return order;
		}
	}
}
