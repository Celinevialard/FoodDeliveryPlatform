using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface IOrdersDB
	{
		List<OrderDetail> GetOrderDetailsByOrder(int orderId);
		List<Order> GetOrdersByCourrier(int courrierId);
		List<Order> GetOrdersByCustomer(int customerId);
		Order InsertOrder(Order order);
		OrderDetail InsertOrderDetails(OrderDetail orderDetail);
		bool UpdateOrder(int orderId, OrderStatusEnum status);
        Order GetOrder(int id);
    }
}