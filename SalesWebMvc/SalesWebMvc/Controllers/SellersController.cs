using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Declarar uma injeção de dependência para o SellerService
        private readonly SellerService _sellerService;

        // Declarar uma injeção de dependência para o DepartmentService
        private readonly DepartmentService _departmentService;

        // Construtor #1
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            // Buscar no banco de dados todos os departamentos
            var departments = _departmentService.FindAll();

            // Instanciar um objeto ViewModel
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
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
