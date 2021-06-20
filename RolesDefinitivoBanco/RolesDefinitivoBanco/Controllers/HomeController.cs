using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RolesDefinitivoBanco.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RolesDefinitivoBanco.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<AppUser> _userManager; //La clase UserManager la utilizamos para gestionar usuarios y eso incluye gestionar sus roles.Para restringir el acceso a determinados roles de usuario, iremos al controller correspondiente e inyectaremos la clase UserManager.


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }
       
        public IActionResult Index() //Si vamos a meter valores asíncronos el método tiene que ser asíncrono también.
        {
   

            return View();
        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
