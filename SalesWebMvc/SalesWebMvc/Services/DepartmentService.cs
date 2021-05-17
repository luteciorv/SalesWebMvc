using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        // Criar dependência para o SalesWebMvcContext
        private readonly SalesWebMvcContext _contex;

        // Contrutor #1
        public DepartmentService(SalesWebMvcContext contex)
        {
            _contex = contex;
        }

        // Retornar todos os departamentos, em ordem alfabética // ASSÍNCRONA
        public async Task<List<Department>> FindAllAsync()
        { 
            return await _contex.Department.OrderBy(d => d.Name).ToListAsync(); 
        }
    }
}
