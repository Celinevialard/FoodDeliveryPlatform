using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FoodDeliveryPlatform.Controllers
{
    public class OrderController : Controller
    {
        IOrderManager OrderManager { get; set; }
        IPersonManager PersonManager { get; set; }
        public OrderController(IOrderManager orderManager, IPersonManager personManager)
        {
            OrderManager = orderManager;
            PersonManager = personManager;
        }
        /// <summary>
        /// CV
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login","Home");
            }

            var orders = OrderManager.GetOrdersByCourrier(1);
            List<OrderVM> ordersVm = new();
            foreach (var order in orders)
            {
                //Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);
                ordersVm.Add(new()
                {
                    //CustomerName = customer.Firstname + " " + customer.Lastname,
                    OrderDate = order.OrderDate,
                    OrderId = order.OrderId,
                    OrderNote= order.OrderNote,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount
                });
            }
            return View(ordersVm);
        }
        /// <summary>
        /// CV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
            //Order order = OrderManager.GetOrder(id);
            //Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);

            //List<OrderDetailVM> orderDetailsVm = new();
            //foreach (var orderDetail in order.Details)
            //{
            //    var dish = DishManager.GetDish(orderDetail.DishId);
            //    orderDetailsVm.Add(new()
            //    {
            //        DishName = dish.Name,
            //        OrderDetailsNote = orderDetail.OrderDetailsNote,
            //        Quantity=orderDetail.Quantity
            //    });
            //}
            //return View(new OrderVM
            //{
            //    CustomerName = customer.Firstname + " " + customer.Lastname,
            //    OrderDate = order.OrderDate,
            //    OrderId = order.OrderId,
            //    OrderNote = order.OrderNote,
            //    Status = order.Status,
            //    TotalAmount = order.TotalAmount,
            //    Details = orderDetailsVm
            //});
        }

        public IActionResult Create(Order order)
        {

            return View();
        }
        /// <summary>
        /// Edit Only Status -- FB
        /// </summary>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        public IActionResult Edit(OrderVM orderVM)
        {
            return View();
        }
    }
}
