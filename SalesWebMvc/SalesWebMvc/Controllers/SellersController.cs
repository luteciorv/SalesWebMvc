using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Declarar uma injeção dependência ao SellerService
        private readonly SellerService _sellerService;

        // Construtor #1
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            // Lista de todos os vendedores
            var sellerList = _sellerService.FindAll();

            return View(sellerList);
        }
    }
}
