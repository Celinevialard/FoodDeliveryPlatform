using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IOrderManager
    {
        decimal CalculTotalAmount(Order order);
        Order CreateOrder(Order order);
        List<DateTime> GetDateDelivery(Order order);
        List<Order> GetOrdersByCourrier(int courrierId);
        List<Order> GetOrdersByCustomer(int customerId);

        Order GetOrder(int id);
        bool DeliverOrder(int orderId);
        bool CancelOrder(int orderId, int orderNumber, string FirstName, string LastName);

    }
}