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
    }
}