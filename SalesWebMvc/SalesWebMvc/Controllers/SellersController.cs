using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;
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

        // ASSÍNCRONO
        public async Task<IActionResult> Index()
        {
            // Lista de todos os vendedores
            var sellerList = await _sellerService.FindAllAsync();

            return View(sellerList);
        }

        // Ação => Criar/Cadastrar um novo vendedor // GET // ASSÍNCRONO
        public async Task<IActionResult> Create()
        {
            // Buscar no banco de dados todos os departamentos
            var departments = await _departmentService.FindAllAsync();

            // Instanciar um objeto ViewModel
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        // Ação => Receber um objeto e instanciar ele // POST // ASSÍNCRONO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            // Caso o objeto passado não seja válido
            if (!ModelState.IsValid)
            {
                // Buscar todos os departamentos
                var departments = await _departmentService.FindAllAsync();

                var viewMode = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewMode);
            }

            // Inserir o vendedor no banco de dados
            await _sellerService.InsertAsync(seller);

            // Redirecionar a requisição para o Index, exibindo a tela de vendedores
            return RedirectToAction(nameof(Index));
        }

        // Action => Remover vendedor // GET // ASSÍNCRONO
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não fornecido" });
            }

            // Buscar o vendedor
            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não encontrado" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Action => Remover vendedor // POST // ASSÍNCRONO
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Remover vendedor
                await _sellerService.RemoveAsync(id);

                // Redirecionar para a listagem dos vendedores
                return RedirectToAction(nameof(Index));
            }

            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        // ACTION => Detalhes do vendedor // GET // ASSÍNCRONO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não fornecido" });
            }

            // Buscar o vendedor
            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não encontrado" });
            }

            return View(seller);
        }

        // ACTION => Editar vendedor => Abir uma tela para editar // GET // ASSÍNCRONO
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não fornecido" });
            }

            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não encontrado" });
            }

            // Carregar os departamentos para povoar a caixinha de seleção
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        // ACTION => Editar vendedor => Abir uma tela para editar // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // Caso o objeto passado não seja válido
            if (!ModelState.IsValid)
            {
                // Buscar todos os departamentos
                var departments = await _departmentService.FindAllAsync();

                var viewMode = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewMode); 
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error),
                new { message = "Número de identificação não correspondem" });
            }

            try
            {
                // Atualizar as informações do vendedor
                await _sellerService.UpdateAsync(seller);

                // Redirecionar a requisição para a listagem de vendedores
                return RedirectToAction(nameof(Index));
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error),
                new { message = e.Message });
            }
        }

        // ACTION => Retornar a janela de Erro
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Pegar o RequestId da aplicação
            };

            return View(viewModel);
        }
    }
}
