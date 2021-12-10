using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryPlatform.Controllers
{
    public class RestaurantController : Controller
    {
        /// <summary>
        /// FB
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// FB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
