using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;

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

        // Action => Remover vendedor // GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            { return NotFound(); }

            // Buscar o vendedor
            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            { return NotFound(); }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Action => Remover vendedor // POST
        public IActionResult Delete(int id)
        {
            // Remover vendedor
            _sellerService.Remove(id);

            // Redirecionar para a listagem dos vendedores
            return RedirectToAction(nameof(Index));
        }

        // ACTION => Detalhes do vendedor // GET
        public IActionResult Details(int? id)
        {
            if (id == null)
            { return NotFound(); }

            // Buscar o vendedor
            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            { return NotFound(); }

            return View(seller);
        }

        // ACTION => Editar vendedor => Abir uma tela para editar // GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return NotFound();
            }

            // Carregar os departamentos para povoar a caixinha de seleção
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        // ACTION => Editar vendedor => Abir uma tela para editar // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                // Atualizar as informações do vendedor
                _sellerService.Update(seller);

                // Redirecionar a requisição para a listagem de vendedores
                return RedirectToAction(nameof(Index));
            }

            catch(NotFoundException)
            {
                return NotFound();
            }

            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
