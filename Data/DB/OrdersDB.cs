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

		public Order InsertOrder(Order order)
        {
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
					string query = @"INSERT INTO Orders (CustomerId, CourrierId, Status, Ordernote, OrderDate, TotalAmount) 
									VALUES (@CustomerId, @CourrierId, @StatusId, @Ordernote, @OrderDate, @TotalAmount); 
									SELECT SCOPE_IDENTITY";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("CustomerId", order.CustomerId);
					cmd.Parameters.AddWithValue("CourrierId", order.CourrierId);
					cmd.Parameters.AddWithValue("Status", order.Status);
					cmd.Parameters.AddWithValue("OrderNote", order.OrderNote);
					cmd.Parameters.AddWithValue("OrderDare", order.OrderDate);
					cmd.Parameters.AddWithValue("TotalAmout", order.TotalAmount);

					cn.Open();
					order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (OrderDetail detail in order.Details)
                    {
						InsertOrderDetails(detail);
                    }

				}
            }
            catch (Exception e)
            {

                throw e;
            }
			return order;
        }

		public OrderDetail InsertOrderDetails(OrderDetail orderDetail)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = @"INSERT INTO OrderDetails (IdDish, OrderId, Quantity, OrderNote) VALUES (@IdDish, @OrderId, @Quantity, @OrderNote); 
									SELECT SCOPE_IDENTITY()";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("IdDish", orderDetail.DishId);
					cmd.Parameters.AddWithValue("OrderId", orderDetail.OrderId);
					cmd.Parameters.AddWithValue("Quantity", orderDetail.Quantity);
					cmd.Parameters.AddWithValue("OrderNote", orderDetail.OrderDetailsNote);

					cn.Open();
					orderDetail.OrderDetailsId = Convert.ToInt32(cmd.ExecuteScalar());

				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return orderDetail;
		}
	}
}
