using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace FoodDeliveryPlatform.Controllers
{
    public class OrderController : Controller
    {
        IOrderManager OrderManager { get; set; }
        IPersonManager PersonManager { get; set; }

        IDishManager DishManager { get; set; }
        public OrderController(IOrderManager orderManager, IPersonManager personManager, IDishManager dishManager)
        {
            OrderManager = orderManager;
            PersonManager = personManager;
            DishManager = dishManager;
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
            UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));
            if(person == null || person.CourrierInfo == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            var orders = OrderManager.GetOrdersByCourrier(person.CourrierInfo.CourrierId);
            List<OrderVM> ordersVm = new();
            foreach (var order in orders)
            {
                Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);
                ordersVm.Add(new()
                {
                    CustomerName = customer.Firstname + " " + customer.Lastname,
                    CustomerAddress = customer.CustomerInfo.Address,
                    CustomerLocation = customer.CustomerInfo.Location.NPA + " " + customer.CustomerInfo.Location.Locality,
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
            Order order = OrderManager.GetOrder(id);
            Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);

            List<OrderDetailVM> orderDetailsVm = new();
            foreach (var orderDetail in order.Details)
            {
                var dish = DishManager.GetDish(orderDetail.DishId);
                orderDetailsVm.Add(new()
                {
                    DishName = dish.Name,
                    OrderDetailsNote = orderDetail.OrderDetailsNote,
                    Quantity = orderDetail.Quantity
                });
            }
            return View(new OrderVM
            {
                CustomerName = customer.Firstname + " " + customer.Lastname,
                CustomerAddress = customer.CustomerInfo.Address,
                CustomerLocation = customer.CustomerInfo.Location.NPA + " " + customer.CustomerInfo.Location.Locality,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderNote = order.OrderNote,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Details = orderDetailsVm
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            return View();
        }
        /// <summary>
        /// update Only Status -- FB
        /// </summary>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            OrderManager.DeliverOrder(id);
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public void AddDishes(Dish dish)
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return;
            }
            CartVM cartVM;
            if (HttpContext.Session.GetString("Cart") == null)
            {
                cartVM = new CartVM
                {
                    CartDetails = new List<CartDetailsVM>(),
                    RestaurantId = dish.RestaurantId
                };
                cartVM.CartDetails.Add(new CartDetailsVM
                {
                    DishId = dish.DishId,
                    DishPrice = dish.Price,
                    DishQuantity = 1
                });
            }
            else
            {
                cartVM = JsonSerializer.Deserialize<CartVM>(HttpContext.Session.GetString("Cart"));
                bool exist = false;
                foreach(var item in cartVM.CartDetails)
                {
                    if(item.DishId == dish.DishId)
                    {
                        item.DishQuantity++;
                        exist = true;
                    }
                }
                if (!exist)
                {
                    cartVM.CartDetails.Add(new CartDetailsVM
                    {
                        DishId = dish.DishId,
                        DishPrice = dish.Price,
                        DishQuantity = 1
                    });
                }
            }
            HttpContext.Session.SetString("Cart", cartVM.ToString());
        }
    }
}
