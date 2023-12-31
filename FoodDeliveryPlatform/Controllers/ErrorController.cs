﻿using FoodDeliveryPlatform.Models;
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
        /// <summary>
        /// Affichage d'une page d'erreur
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public IActionResult Index(HttpStatusCode errorCode, string errorMessage = null)
        {
            switch (errorCode)
            {
                case HttpStatusCode.Unauthorized:
                    errorMessage ??= "Accès non autorisé.";
                    break;
                case HttpStatusCode.InternalServerError:
                default:
                    errorMessage ??= "Une erreur est survenue.";
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
