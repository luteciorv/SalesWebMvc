using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Busca simples
        public IActionResult SimpleSearch()
        {
            return View();
        }

        // Guspa por grupos
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
