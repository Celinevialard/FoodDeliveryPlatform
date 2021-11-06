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
	public class OrdersDB : IOrdersDB
	{

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

		/// <summary>
		/// Récupération de la liste des commandes d'un utilisateur
		/// </summary>
		/// <param name="customerId"></param>
		/// <returns></returns>
		public List<Order> GetOrdersByCustomer(int customerId)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			List<Order> results = null;
			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "SELECT Orders WHERE customerId = @customerId ORDER BY orderDate";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("customerId", customerId);

					cn.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Order>();

							Order order = ReadOrder(dr);
							order.Details = GetOrderDetailsByOrder(order.OrderId);
							results.Add(order);

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
		/// Récupération de la liste de commande non livrer pour un livreur
		/// </summary>
		/// <param name="courrierId"></param>
		/// <returns></returns>
		public List<Order> GetOrdersByCourrier(int courrierId)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			List<Order> results = null;
			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "SELECT Orders WHERE courrierId = @courrierId AND Status = @statusID ORDER BY orderDate";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("courrierId", courrierId);
					cmd.Parameters.AddWithValue("statusID", OrderStatusEnum.Delivering);

					cn.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<Order>();

							Order order = ReadOrder(dr);
							order.Details = GetOrderDetailsByOrder(order.OrderId);
							results.Add(order);

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
		/// Récupération des details d'une commandes
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		public List<OrderDetail> GetOrderDetailsByOrder(int orderId)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			List<OrderDetail> results = null;
			try
			{
				using (SqlConnection cn = new SqlConnection(connectionString))
				{
					string query = "SELECT OrderDetails WHERE orderID = @orderId";
					SqlCommand cmd = new SqlCommand(query, cn);
					cmd.Parameters.AddWithValue("orderId", orderId);

					cn.Open();
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (results == null)
								results = new List<OrderDetail>();

							results.Add(ReadOrderDetails(dr));

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

		private Order ReadOrder(SqlDataReader dr)
		{
			Order entity = new Order();

			entity.OrderId = (int)dr["orderId"];
			entity.OrderNote = (string)dr["orderNote"];
			entity.Status = (OrderStatusEnum)dr["Status"];


			if (dr["courrierId"] != DBNull.Value)
				entity.CourrierId = (int)dr["courrierId"];

			if (dr["customerId"] != DBNull.Value)
				entity.CustomerId = (int)dr["customerId"];

			if (dr["orderDate"] != DBNull.Value)
				entity.OrderDate = (DateTime)dr["orderDate"];

			if (dr["totalAmount"] != DBNull.Value)
				entity.TotalAmount = (decimal)dr["totalAmount"];

			return entity;
		}

		private OrderDetail ReadOrderDetails(SqlDataReader dr)
		{
			OrderDetail entity = new OrderDetail();

			entity.OrderDetailsId = (int)dr["orderDetailsId"];
			entity.OrderId = (int)dr["orderId"];

			if (dr["idDish"] != DBNull.Value)
				entity.DishId = (int)dr["idDish"];

			if (dr["quantity"] != DBNull.Value)
				entity.Quantity = (int)dr["quantity"];

			if (dr["orderNote"] != DBNull.Value)
				entity.OrderDetailsNote = (string)dr["orderNote"];

			return entity;
		}

	}
}
