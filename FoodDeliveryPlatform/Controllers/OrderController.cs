﻿using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            if(person == null )
            {
                return RedirectToAction("Logout", "Home");
            }
            List<Order> ordersDelivery = null; 
            List<OrderVM> ordersDeliveryVm = null;
            if (person.CourrierInfo != null)
            {
                ordersDelivery = OrderManager.GetOrdersByCourrier(person.CourrierInfo.CourrierId);

                if (ordersDelivery != null && ordersDelivery.Any())
                {
                    ordersDeliveryVm = new();
                    foreach (var order in ordersDelivery)
                    {
                        Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);
                        ordersDeliveryVm.Add(new()
                        {
                            CustomerName = customer.Firstname + " " + customer.Lastname,
                            CustomerAddress = customer.CustomerInfo.Address,
                            CustomerLocation = customer.CustomerInfo.Location.LocationName,
                            OrderDate = order.OrderDate,
                            OrderId = order.OrderId,
                            OrderNote = order.OrderNote,
                            Status = order.Status,
                            TotalAmount = order.TotalAmount
                        });
                    }
                }
            }
            List<Order> ordersCustomer = null;
            List<OrderVM> ordersCustomerVm = null;
            if (person.CustomerInfo != null)
            {
                ordersCustomer = OrderManager.GetOrdersByCustomer(person.CustomerInfo.CustomerId);

                if (ordersCustomer != null && ordersCustomer.Any())
                {
                    ordersCustomerVm = new();
                    foreach (var order in ordersCustomer)
                    {
                        ordersCustomerVm.Add(new()
                        {
                            OrderDate = order.OrderDate,
                            OrderId = order.OrderId,
                            OrderNote = order.OrderNote,
                            Status = order.Status,
                            TotalAmount = order.TotalAmount
                        });
                    }
                }
            }
           
            return View(new OrderIndexVM
            {
                OrdersCustomer = ordersCustomerVm,
                OrdersDelivery = ordersDeliveryVm
            });
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

            List<OrderDetailVM> orderDetailsVm = null;
            if (order.Details != null && order.Details.Any())
            {
                orderDetailsVm = new();
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
            }
            return View(new OrderVM
            {
                CustomerName = customer.Firstname + " " + customer.Lastname,
                CustomerAddress = customer.CustomerInfo.Address,
                CustomerLocation = customer.CustomerInfo.Location.LocationName,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderNote = order.OrderNote,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Details = orderDetailsVm
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));
            if(person.CustomerInfo == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            CartVM cartVM;
            if (HttpContext.Session.GetString("Cart") == null)
            {
                return View(null);
            }
            cartVM = JsonSerializer.Deserialize<CartVM>(HttpContext.Session.GetString("Cart"));

            Order order = CartToOrder(cartVM);
            order.CustomerId = person.CustomerInfo.CustomerId;
            cartVM.DatesDelivery = OrderManager.GetDateDelivery(order);
            return View(cartVM);
        }
       
        [HttpPost]
        public IActionResult Create(CartVM cart)
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));
            if (person.CustomerInfo == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            Order order = CartToOrder(cart);
            order.CustomerId = person.CustomerInfo.CustomerId;
            OrderManager.CreateOrder(order);

            // delete session cart
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

        private Order CartToOrder(CartVM cart)
        {
            Order order = new();
            order.OrderDate = cart.DateDelivery;
            order.OrderNote = cart.OrderNote;
            order.Details = new();

            foreach (CartDetailsVM cartDetails in cart.CartDetails)
            {
                order.Details.Add(new OrderDetail
                {
                    DishId = cartDetails.DishId,
                    Quantity = cartDetails.DishQuantity
                });
            }
            return order;
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

        public IActionResult Cancel(int id)
        {
            OrderManager.CancelOrder(id);
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public JsonResult AddDish(int id)
        {
            Dish dish = DishManager.GetDish(id);
            if (HttpContext.Session.GetString("User") == null)
            {
                return new JsonResult(new { message = "pas connecter"});
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
                    DishName = dish.Name,
                    DishPrice = dish.Price,
                    DishQuantity = 1
                });
            }
            else
            {
                cartVM = JsonSerializer.Deserialize<CartVM>(HttpContext.Session.GetString("Cart"));
                if (dish.RestaurantId != cartVM.RestaurantId)
                    return new JsonResult(new { message = "Le plat ne provient pas du même restaurant" });

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
                        DishName = dish.Name,
                        DishPrice = dish.Price,
                        DishQuantity = 1
                    });
                }
            }
            HttpContext.Session.SetString("Cart", cartVM.ToString());
            return new JsonResult(new { message = "Reussi" });
        }

        [HttpPost]
        public JsonResult RemoveDish(int id)
        {
            Dish dish = DishManager.GetDish(id);
            if (HttpContext.Session.GetString("User") == null)
            {
                return new JsonResult(new { message = "pas connecter" });
            }
            CartVM cartVM;
            if (HttpContext.Session.GetString("Cart") == null)
            {
                return new JsonResult(new { message = "Vous n'avez pas de panier." });
            }
            else
            {
                cartVM = JsonSerializer.Deserialize<CartVM>(HttpContext.Session.GetString("Cart"));
                bool exist = false;
                CartDetailsVM itemToRemove = null;
                foreach (var item in cartVM.CartDetails)
                {
                    if (item.DishId == dish.DishId)
                    {
                        item.DishQuantity--;
                        exist = true;
                        if (item.DishQuantity <= 0)
                            itemToRemove = item;
                    }
                }
                if (itemToRemove != null)
                    cartVM.CartDetails.Remove(itemToRemove);
                if (!exist)
                {
                    return new JsonResult(new { message = "Ce plat n'est pas dans le panier." });
                }
            }
            HttpContext.Session.SetString("Cart", cartVM.ToString());
            return new JsonResult(new { message = "Reussi" });
        }
    }
}
