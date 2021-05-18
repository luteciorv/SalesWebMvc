using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {        
        // Declarar injeção de independência
        private readonly SalesRecordService _salesRecordService;

        // Construtor #1
        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {          
            return View();
        }

        // Busca simples // ASSÍNCRONA
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            // Verificar se algum valor para a variável "Min Date" foi informado
            minDate = !minDate.HasValue ? new DateTime(DateTime.Now.Year, 1, 1) : minDate;

            // Verificar se algum valor para a variável "Min Date" foi informado
            maxDate = !maxDate.HasValue ? DateTime.Now : maxDate;

            // Passar os valores das variáveis "Min Date" e "Max Date"
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            // Encontrar todos as vendas realizadas no período informado
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }

        // Guspa por grupos
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
