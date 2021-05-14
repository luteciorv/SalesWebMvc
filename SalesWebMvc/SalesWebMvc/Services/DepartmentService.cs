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

        // Retornar todos os departamentos, em ordem alfabética
        public List<Department> FindAll()
        { return _contex.Department.OrderBy(d => d.Name).ToList(); }
    }
}
