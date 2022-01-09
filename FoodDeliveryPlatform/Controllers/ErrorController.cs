using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(HttpStatusCode errorCode, string errorMessage = null)
        {
            switch (errorCode)
            {
                case HttpStatusCode.Unauthorized:
                    errorMessage ??= "Accès non autoriser.";
                    break;
                case HttpStatusCode.InternalServerError:
                default:
                    errorMessage ??= "Une erreur est survenu.";
                    break;

            }
            return View(new ErrorVM()
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            });
        }
    }
}
