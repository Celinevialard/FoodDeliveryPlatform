using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int errorCode, string errorMessage=null)
        {
            switch (errorCode)
            {
                case 403:
                    errorMessage ??= "Accès non autoriser";
                    break;
            }
            return View(new ErrorVM()
            {
                ErrorCode=errorCode,
                ErrorMessage=errorMessage??"Une erreur est survenu."
            });
        }
    }
}
