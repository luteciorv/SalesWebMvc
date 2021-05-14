using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        // Ação => Criar/Cadastrar um novo vendedor // GET
        public IActionResult Create()
        {
            return View();
        }

        // Ação => Receber um objeto e instanciar ele // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            // Inserir o vendedor no banco de dados
            _sellerService.Insert(seller);

            // Redirecionar a requisição para o Index, exibindo a tela de vendedores
            return RedirectToAction(nameof(Index));           
        }
    }
}
