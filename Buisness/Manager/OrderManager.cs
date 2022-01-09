using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderManager : IOrderManager
    {
        private IOrdersDB OrdersDb { get; }
        private IDishesDB DishesDb { get; }
        private IRestaurantsDB RestaurantsDb { get; }
        private ICourriersDB CourriersDb { get; }
        private ICustomersDB CustomersDb { get; }
        private IPersonsDB PersonsDb { get; }
        public ILocationsDB LocationsDb { get; private set; }

        public OrderManager(IOrdersDB ordersDB, IDishesDB dishesDB, IRestaurantsDB restaurantsDB, ICourriersDB courriersDB, ICustomersDB customersDB, ILocationsDB locationsDB, IPersonsDB personsDB)
        {
            OrdersDb = ordersDB;
            CustomersDb = customersDB;
            DishesDb = dishesDB;
            CourriersDb = courriersDB;
            RestaurantsDb = restaurantsDB;
            LocationsDb = locationsDB;
            PersonsDb = personsDB;
        }

        /// <summary>
        /// Liste les commandes pour un livreur
        /// </summary>
        /// <param name="courrierId"></param>
        /// <returns></returns>
        public List<Order> GetOrdersByCourrier(int courrierId)
        {
            List<Order> orders = OrdersDb.GetOrdersByCourrier(courrierId);
            if (orders == null || !orders.Any())
                return null;
            foreach (Order o in orders)
            {
                o.Location = LocationsDb.GetLocationById(o.LocationId);
            }

            return orders;
        }

        /// <summary>
        /// Liste les commandes pour un client
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> orders = OrdersDb.GetOrdersByCustomer(customerId);
            if(orders ==null || !orders.Any())
                return null;
            foreach (Order o in orders)
            {
                o.Location = LocationsDb.GetLocationById(o.LocationId);
            }

            return orders;
        }

        /// <summary>
        /// Récupère une commande
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            Order o = OrdersDb.GetOrder(id);
            if (o == null )
                return null;
            o.Location = LocationsDb.GetLocationById(o.LocationId);
            return o;
        }

        /// <summary>
        /// Création et validation d'une commande
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order CreateOrder(Order order)
        {
            int courrierId = SetCourrierByOrder(order);
            if (courrierId == 0 || order.OrderDate <= DateTime.Now) {
                return null;
            }
            CheckAllDishFromSameRestaurant(order);
            order.TotalAmount = CalculTotalAmount(order);
            order.CourrierId = courrierId;
            order.Status = OrderStatusEnum.Delivering;
            return OrdersDb.InsertOrder(order);
        }

        /// <summary>
        /// Calcul du montant total de la commande
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public decimal CalculTotalAmount(Order order)
        {
            decimal totalAmout = 0;
            foreach (OrderDetail detail in order.Details)
            {
                Dish dish = DishesDb.GetDishById(detail.DishId);
                totalAmout += dish.Price * detail.Quantity;
            }
            return totalAmout;
        }

        /// <summary>
        /// Liste les tranches horaires pour se faire livrer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<DateTime> GetDateDelivery(Order order)
        {
            List<DateTime> dateDelivery = new List<DateTime>();
            List<DateTime> datePossible = new List<DateTime>();
            (int depart, int arrivee) = GetLocalites(order);
            List<Courrier> courriers = CourriersDb.GetCourrierByLocalite(depart, arrivee);

            if (courriers == null || !courriers.Any())
                return dateDelivery;

                // TODO enlever tranche si pas de delever dispo
                foreach (Courrier courrier in courriers)
                {
                    courrier.Orders = OrdersDb.GetOrdersByCourrier(courrier.CourrierId);
                }
                for (int i = 0; i < 4 * 6; i++)
                {
                    DateTime time;
                    if (i == 0)
                    {
                        time = DateTime.Now;
                        time = time.AddMinutes(30 - time.Minute % 15);
                        time = time.AddSeconds(0 - time.Second);
                        time = time.AddMilliseconds(0 - time.Millisecond);
                    }
                    else
                    {
                        time = datePossible[i - 1].AddMinutes(15);
                    }
                    datePossible.Add(time);
                    int index = GetCourrierIdDispo(courriers, time);

                    if (index > 0)
                        dateDelivery.Add(time);
                }

                return dateDelivery;
           
        }

        /// <summary>
        /// Contrôle que tout le plats de la commande viennent du même restaurant
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool CheckAllDishFromSameRestaurant(Order order)
        {
            int restaurantId = 0;
            foreach (OrderDetail detail in order.Details)
            {
                Dish dish = DishesDb.GetDishById(detail.DishId);
                if (restaurantId == 0)
                    restaurantId = dish.RestaurantId;
                else if (restaurantId != dish.RestaurantId)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Récupération de la localité du restaurant et du client d'une commande
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private (int, int) GetLocalites(Order order)
        {
            Customer customer = CustomersDb.GetCustomer(order.CustomerId);
            Dish dish = DishesDb.GetDishById(order.Details[0].DishId);
            Restaurant restaurant = RestaurantsDb.GetRestaurantsById(dish.RestaurantId);

            return (restaurant.LocationId, customer.LocationId);
        }

        /// <summary>
        /// Attribue un livreur pour une commande
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int SetCourrierByOrder(Order order)
        {
            (int depart, int arrivee) = GetLocalites(order);
            List<Courrier> courriers = CourriersDb.GetCourrierByLocalite(depart, arrivee);

            return GetCourrierIdDispo(courriers, order.OrderDate);
        }

        /// <summary>
        /// Retourne le premier livreur dispo à un DateTime
        /// </summary>
        /// <param name="courriers"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private int GetCourrierIdDispo(List<Courrier> courriers, DateTime date)
        {
            foreach (Courrier courrier in courriers)
            {
                List<Order> orders = courrier.Orders ?? OrdersDb.GetOrdersByCourrier(courrier.CourrierId);
                int nbOrder = 0;
                if(orders != null && orders.Any())
                {
                    foreach (Order o in orders.Where(e=>e.Status==OrderStatusEnum.Delivering))
                    {
                        if (o.OrderDate.AddMinutes(16) >= date && o.OrderDate.AddMinutes(-16) <= date)
                            nbOrder++;
                    }
                }
               
                if (nbOrder < 5)
                    return courrier.CourrierId;
            }
            return 0;
        }

        /// <summary>
        /// Change le statut de livraison en livré
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool DeliverOrder(int orderId)
        {
            return OrdersDb.UpdateOrder(orderId, OrderStatusEnum.Delivered);
        }

        /// <summary>
        /// Autorise l'annulation de la commande jusqu'à 3 heures avant son DateTime de livraison
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CancelOrder(int orderId, int orderNumber, string firstName, string lastName)
        {
            if (orderId != orderNumber)
                return false;
            Order order = OrdersDb.GetOrder(orderId);

            Person customer = PersonsDb.GetPersonByCustomer(order.CustomerId);

            if (customer.Firstname != firstName || customer.Lastname != lastName)
                return false;

            if (DateTime.Now.AddHours(3) <= order.OrderDate)
                return OrdersDb.UpdateOrder(orderId, OrderStatusEnum.Cancelled);
            return false;
        }
    }
}
